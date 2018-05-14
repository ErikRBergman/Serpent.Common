Param(
	[Parameter(Mandatory=$false,Position=1)]
	[string] $projectPath,
	[Parameter(Mandatory=$false,Position=2)]
    [string] $configuration
)

# functions

function updateNodeVersion
{
    Param ([System.Xml.XmlElement] $versionNode, [string] $filename)

    if ($versionNode -eq $null)
    {
        return $false
    }

    $nodename = $versionNode.Name

     $oldVersion = $versionNode.InnerText
     $version = getUpdatedVersion $oldVersion
        
        if (-not $oldVersion.Equals($version))
        {
            $versionNode.InnerText = $version
            
            Write-Host "$filename : Updating $nodename from $oldVersion to $version"

            return $true
        }
   
   return $false

}

function getUpdatedVersion
{
    Param ([string] $version)
    $versionArray = $version.Split(".")
    if ($versionArray.Length -eq 4)
    {
        $versionArray[3] = ([int]$versionArray[3]) + 1
        $version =  [string]::Join(".",$versionArray)

        return $version        
    }

    return $inputVerison
}


# Update version number

function updateProjectVersionNumber
{
    $filter = "*.csproj"

    echo "Scanning $filter to update version..."

    Get-ChildItem -Filter "$filter" | ForEach-Object { 
        updateFileVersion $_.FullName 
        }
}

function updateFileVersion
{
    Param ([string] $filename)

    $content = Get-Content $filename 
    [xml]$xml = $content

    $shortFilename = [System.IO.Path]::GetFileName($filename)

    $wasUpdated = $false

    $propertyGroups = $xml.SelectNodes("//Project/PropertyGroup")

    $updatedVersion = $false
    $updatedFileVersion = $false
    $updatedAssemblyVersion = $false

    $firstNonConditionalPropertyGroup = $null

    foreach ($propertyGroup in $propertyGroups)
    {
        if ($firstNonConditionalPropertyGroup -eq $null)
        {            
            $condition = $propertyGroup.GetAttribute("Condition")
    
            if ($condition -eq "")
            {
                $firstNonConditionalPropertyGroup = $propertyGroup
            }
        }

        $r = updateNodeVersion $propertyGroup.SelectSingleNode("Version") $shortFilename

        if ($r -eq $true)
        {
            $wasUpdated = $true
            $updatedVersion = $true
        }

        $r = updateNodeVersion $propertyGroup.SelectSingleNode("FileVersion") $shortFilename

        if ($r -eq $true)
        {
            $wasUpdated = $true
            $updatedFileVersion = $true
        }

        $r = updateNodeVersion $propertyGroup.SelectSingleNode("AssemblyVersion") $shortFilename

        if ($r -eq $true)
        {
            $wasUpdated = $true
            $updatedAssemblyVersion = $true
        }
    }

    if ($updatedVersion -eq $false)
    {
        Write-Host "Version not found in any propertygroup. Adding Version 0.0.0.1"
        $version =  $xml.CreateElement("Version")
        $version.InnerText = "0.0.0.1"
        $firstNonConditionalPropertyGroup.AppendChild($version)
        $wasUpdated = $true
    }

    if ($updatedFileVersion -eq $false)
    {
        Write-Host "FileVersion not found in any propertygroup. Adding FileVersion 0.0.0.1"
        $version =  $xml.CreateElement("FileVersion")
        $version.InnerText = "0.0.0.1"
        $firstNonConditionalPropertyGroup.AppendChild($version)
        $wasUpdated = $true
    }

    if ($updatedAssemblyVersion -eq $false)
    {
        Write-Host "AssemblyVersion not found in any propertygroup. Adding AssemblyVersion 0.0.0.1"
        $version =  $xml.CreateElement("AssemblyVersion")
        $version.InnerText = "0.0.0.1"
        $firstNonConditionalPropertyGroup.AppendChild($version)
        $wasUpdated = $true
    }

    if ($wasUpdated -eq $true)
    {
        $xml.Save($_.FullName);
    }
}

class NugetSettings
{
    [String] $source
    [String] $apikey
    [String] $nugetExecutable
    [String] $filename
	
}

function getNugetConfig
{
	$nugetSettings = $null

	$path = $path = (Get-Item -Path $PSScriptRoot)

	do {
		$file = $path.FullName + "\" + "nuget_publish_settings.config"

		if (Test-Path $file) {
    
				$settings = Get-Content -Raw -Path $file | ConvertFrom-Json

				$nugetSettings = New-Object NugetSettings
				$nugetSettings.source = $settings.source
				$nugetSettings.apikey = $settings.apikey
				$nugetSettings.filename = $file
				$nugetSettings.nugetExecutable = $settings.nugetExecutable
            
				break
			}
			else {

				$path = $path.Parent
			}        

		} while ($path -ne $null)

	return $nugetSettings
}

function copyToCacheDirectory
{
    Param ([string] $filter)

    Get-ChildItem -Filter "$filter" | ForEach-Object {     
        $filename = $_.FullName

        if (Test-Path -Path "c:\nugetpackages") {
			
            Write-Host "Copying nuget cache directory: $filename"
	        copy-Item $filename "c:\NugetPackages"
            }
    }
}

function deleteOldPackages
{
	$filter = "bin\" + $configuration + "\*.nupkg"
    Get-ChildItem -Filter "$filter" | ForEach-Object {     
		Remove-Item $_.FullName	
    }
}

function pushToNugetServer
{
	 Param ([string] $filter, [NugetSettings] $settings)

	$source = $settings.source
	$nugetconfigfile = $settings.filename

    Write-Host "Using nuget configuration from $nugetconfigfile ..."

    Get-ChildItem -Filter "$filter" | ForEach-Object {     
        $filename = $_.FullName

        Write-Host "Pushing $filename to $source ..."
        
		# Write-Host $settings.nugetExecutable push $filename -apikey $settings.apikey -source $settings.source

        & $settings.nugetExecutable push $filename -apikey $settings.apikey -source $settings.source

	    Remove-Item $filename
    }

}

# Update version number

function updateProjectVersionNumber
{
    $filter = "*.csproj"

    echo "Scanning $filter to update version..."

    Get-ChildItem -Filter "$filter" | ForEach-Object { 
        updateFileVersion $_.FullName 
    }
}

# This is where the action is at

cls

if ($configuration -eq $null) {
		$configuration = 'release'
    }

if ($configuration -eq '') {
		$configuration = 'release'
    }

$oldPath = (Get-Item -Path ".\").FullName

if ($projectPath -eq $null) {
		$projectPath = $PSScriptRoot
	}

cd $projectPath

updateProjectVersionNumber

Write-Host ""

Write-Host "Building ($configuration) and creating nuget package for $file ..."
if ($configuration -eq 'debug') {
        dotnet pack -c debug --include-symbols --force
    }
    else {
        dotnet pack -c release --include-symbols --force
    }

Write-Host ""
Write-Host "Copying packages to local nuget cache directory..."

$filter = "bin\" + $configuration + "\*.nupkg"
copyToCacheDirectory $filter
	
Write-Host ""
Write-Host "Pushing package to nuget server..."

$nugetConfig = getNugetConfig

$filter = "bin\" + $configuration + "\*.symbols.nupkg"

pushToNugetServer $filter $nugetConfig

deleteOldPackages

cd $oldPath
