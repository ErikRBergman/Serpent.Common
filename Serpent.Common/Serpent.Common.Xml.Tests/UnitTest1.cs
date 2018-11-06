using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Serpent.Common.Xml.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(this.xml));

            var reader = new XmlFileParser();

            var endElementDictionary = new Dictionary<string, Action<string>>();
            endElementDictionary["/Product"] = s =>
                {
                    var x = 1;
                };

            await reader.ReadXmlFileAsync<string>(
                stream,
                "crapto",
                "x",
                new Dictionary<string, Action<string, string>>(),
                endElementDictionary,
                elementHandlerFunc: (s, r) =>
                    {
                        if (s.StartsWith("/Product/Items/Item/ItemTextHead"))
                        {
                            var x = r.ReadElementContentAsString();
                        }

                        return true;
                    });
        }




        private string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Product id=""505603"" action=""updated"">
  <ProductNumber><![CDATA[04345334]]></ProductNumber>
  <ProductTitle><![CDATA[LINJELASER 2 LINJER DW088K-XJ]]></ProductTitle>
  <ProductShortDescription><![CDATA[LINJELASER 2 LINJER DW088K-XJ ]]></ProductShortDescription>
  <ProductDescription />
  <ProductSupplier cvl=""CVLProductSupplier""><![CDATA[5067319]]></ProductSupplier>
  <ProductPurchaser cvl=""Users""><![CDATA[CON]]></ProductPurchaser>
  <ProductMainGroup cvl=""CVLProductMainGroup""><![CDATA[14]]></ProductMainGroup>
  <ProductSubGroup cvl=""CVLProductSubGroup""><![CDATA[142]]></ProductSubGroup>
  <ProductDepartment cvl=""CVLProductDepartment""><![CDATA[14201]]></ProductDepartment>
  <ProductBrand cvl=""CVLProductBrand"" />
  <ProductEcoLabeling cvl=""CVLProductEcoLabeling"" />
  <ProductStatus cvl=""CVLProductStatus""><![CDATA[ny]]></ProductStatus>
  <ProductAutosaveVersion />
  <Items>
    <Item id=""505602"" LinkType=""Link_ProductItem"">
      <ItemNumber><![CDATA[04345334]]></ItemNumber>
      <ItemFinfoNumber><![CDATA[005174983]]></ItemFinfoNumber>
      <ItemBygmaProductNumber><![CDATA[DW50]]></ItemBygmaProductNumber>
      <ItemEAN><![CDATA[5035048338575]]></ItemEAN>
      <ItemTitle1><![CDATA[LINJELASER 2 LINJER DW088K-XJ]]></ItemTitle1>
      <ItemTitle2 />
      <ItemSupplierItemNumber><![CDATA[DW088K-XJ]]></ItemSupplierItemNumber>
      <ItemSupplierTitle1><![CDATA[LINJELASER, 2 LINJER]]></ItemSupplierTitle1>
      <ItemSupplierTitle2 />
      <ItemBranch cvl=""CVLItemBranch"">
        <value><![CDATA[0]]></value>
        <value><![CDATA[98]]></value>
      </ItemBranch>
      <ItemPriceexVAT><![CDATA[2014]]></ItemPriceexVAT>
      <ItemPriceVAT><![CDATA[2518]]></ItemPriceVAT>
      <ItemSalesUnit cvl=""CVLItemSalesUnit""><![CDATA[ST]]></ItemSalesUnit>
      <ItemWeight><![CDATA[1,702]]></ItemWeight>
      <ItemLength><![CDATA[0]]></ItemLength>
      <ItemWidth><![CDATA[0]]></ItemWidth>
      <ItemHeight><![CDATA[0]]></ItemHeight>
      <ItemVolume><![CDATA[0]]></ItemVolume>
      <ItemCampaignCode />
      <ItemSEO1><![CDATA[VATTENPASS R�TSTEGE]]></ItemSEO1>
      <ItemSEO2 />
      <ItemSEO3 />
      <ItemSEO4><![CDATA[DEWALT]]></ItemSEO4>
      <ItemSEO5 />
      <ItemSEO6><![CDATA[BLACK DECKER DEWALT]]></ItemSEO6>
      <ItemSEO7 />
      <ItemSEO8 />
      <ItemSEO9 />
      <ItemSEO10 />
      <ItemStatus cvl=""CVLItemStatus""><![CDATA[ny]]></ItemStatus>
      <ItemStatusTrygg cvl=""CVLItemStatusTrygg"" />
      <ItemPublish cvl=""CVLItemPublish""><![CDATA[ja]]></ItemPublish>
      <ItemPTIFinfo><![CDATA[LINJELASER, 2 LINJER]]></ItemPTIFinfo>
      <ItemPTKFinfo><![CDATA[Sj�lvnivellerande krysslinjelaser �r exakt till +/-0,3 mm/m i nivellerande anv�ndning. Sj�lvnivellerande upp till 4� ytvinkel med automatisk inte-i-niv� indikator, markerar en nivellerad linje p� sekunder. 2-knapps funktion och indikator f�r l�g batteri niv� f�r enkel anv�ndning. Inbyggda magnetiska g�ngj�rn f�ster l�tt p� ytor av metall. Skyddande k�pa och tjockt glas skyddar verktyget och hj�lper till att bibeh�lla kalibreringen vid anv�ndning p� arbetsplats. Levereras med 3 st AA-batterier f�r �ver 40 timmars anv�ndningstid. F�rvaringsl�da skyddar lasern och bidrar till att bibeh�lla kalibreringen under f�rvaringen. Fungerar tillsammans med detektor DE0892 upp till 50 m avst�nd. 3 st Alkaline batterier (storlek AA).v�ggh�llare. f�rvaringsl�da..]]></ItemPTKFinfo>
      <ItemPTTFinfo><![CDATA[Laserklass 2, <1mW .Noggrannhet +/-0.3 mm/m.Antal laserstr�lar 2 .Riktning laserstr�lar Vertikal/horisontell .Synbarhet inomhus 10 m.Sj�lvnivellerande r�ckvidd 4 �.Str�mk�lla 4.5 V - 3 st AA .Vikt (med batteri) 0.46 g.Bredd 61 mm.L�ngd 112 mm.H�jd 113 mm]]></ItemPTTFinfo>
      <ItemPTPFinfo />
      <ItemPTI><![CDATA[L�tthanterlig krysslaser med 2 linjer.]]></ItemPTI>
      <ItemPTK><![CDATA[Lasern har sj�lvnivellerande krysslaser som �r exakt till +/-0,3 mm/m i nivellerande anv�ndning. Verktyget har en 2-knapps funktion och indikator f�r l�g batteriniv� f�r enkel anv�ndning. Det har dessutom inbyggda magnetiska g�ngj�rn som f�ster l�tt p� ytor av metall. Utrustad med skyddande k�pa och tjockt glas som hj�lper till att skydda lasern och bibeh�lla kalibreringen vid anv�ndning p� arbetsplats.]]></ItemPTK>
      <ItemPTT />
      <ItemPTP />
      <ItemTextHead1 />
      <ItemTextPart1><![CDATA[Levereras med 3 st Alkaline batterier, v�ggh�llare och f�rvaringsl�da.]]></ItemTextPart1>
      <ItemTextHead2 />
      <ItemTextPart2 />
      <ItemTextHead3 />
      <ItemTextPart3 />
      <ItemTextHead4 />
      <ItemTextPart4 />
      <ItemTextHead5 />
      <ItemTextPart5 />
      <ItemTextHead6 />
      <ItemTextPart6 />
      <ItemTextHead7 />
      <ItemTextPart7 />
      <ItemTextHead8 />
      <ItemTextPart8 />
      <ItemTextHead9 />
      <ItemTextPart9 />
      <ItemTextHead10 />
      <ItemTextPart10 />
      <ItemClass><![CDATA[BS]]></ItemClass>
      <Resources>
        <Resource id=""1346874"" LinkType=""Link_ItemResource"">
          <ResourceName><![CDATA[005174983_BP1]]></ResourceName>
          <ResourceType cvl=""CVLResourceType""><![CDATA[BP]]></ResourceType>
          <ResourceNumber><![CDATA[1]]></ResourceNumber>
          <ResourceFileId><![CDATA[1763467]]></ResourceFileId>
          <ResourceFilename><![CDATA[005174983_BP1.jpg]]></ResourceFilename>
          <ResourceMimeType><![CDATA[image/jpeg]]></ResourceMimeType>
          <Filename>E:\PIM\Hotfolders\Export\StandardOutput\Webb\Pics\1346874.jpg</Filename>
        </Resource>
        <Resource id=""1442287"" LinkType=""Link_ItemResource"">
          <ResourceName><![CDATA[04345334_EBP1]]></ResourceName>
          <ResourceType cvl=""CVLResourceType""><![CDATA[EBP]]></ResourceType>
          <ResourceNumber><![CDATA[1]]></ResourceNumber>
          <ResourceFileId><![CDATA[646073]]></ResourceFileId>
          <ResourceFilename><![CDATA[04345334_EBP1.jpg]]></ResourceFilename>
          <ResourceMimeType><![CDATA[image/jpeg]]></ResourceMimeType>
          <Filename>E:\PIM\Hotfolders\Export\StandardOutput\Webb\Pics\1442287.jpg</Filename>
        </Resource>
        <Resource id=""1346873"" LinkType=""Link_ItemResource"">
          <ResourceName><![CDATA[005174983_PB1]]></ResourceName>
          <ResourceType cvl=""CVLResourceType""><![CDATA[PB]]></ResourceType>
          <ResourceNumber><![CDATA[1]]></ResourceNumber>
          <ResourceFileId><![CDATA[1763466]]></ResourceFileId>
          <ResourceFilename><![CDATA[005174983_PB1.pdf]]></ResourceFilename>
          <ResourceMimeType><![CDATA[application/pdf]]></ResourceMimeType>
          <Filename>E:\PIM\Hotfolders\Export\StandardOutput\Webb\Pics\1346873.pdf</Filename>
        </Resource>
        <Resource id=""1477622"" LinkType=""Link_ItemResource"">
          <ResourceName><![CDATA[04345334_EPB1]]></ResourceName>
          <ResourceType cvl=""CVLResourceType""><![CDATA[EPB]]></ResourceType>
          <ResourceNumber><![CDATA[1]]></ResourceNumber>
          <ResourceFileId><![CDATA[682484]]></ResourceFileId>
          <ResourceFilename><![CDATA[04345334_EPB1.pdf]]></ResourceFilename>
          <ResourceMimeType><![CDATA[application/pdf]]></ResourceMimeType>
          <Filename>E:\PIM\Hotfolders\Export\StandardOutput\Webb\Pics\1477622.pdf</Filename>
        </Resource>
      </Resources>
    </Item>
  </Items>
  <Assortments>
    <Assortment id=""75686"" order=""632"" />
  </Assortments>
</Product>";
    }
}
