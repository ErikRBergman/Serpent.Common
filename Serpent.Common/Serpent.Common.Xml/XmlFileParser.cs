namespace Serpent.Common.Xml
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Serpent.Common.BaseTypeExtensions.Collections;
    using Serpent.Common.Xml.UtilityClasses;

    public class XmlFileParser<TContextType> : XmlFileParser
    {
        private readonly Func<string, XmlTextReader, bool> elementHandlerFunc;

        private readonly ImmutableHashSet<string> nonEndMatches = new ImmutableHashSet<string>();

        private readonly ImmutableHashSet<string> nonMatches = new ImmutableHashSet<string>();

        private readonly WildcardMatcherContainer wildcardMatcherContainer = new WildcardMatcherContainer();

        private IReadOnlyDictionary<string, Action<TContextType, string>> elementMap;

        private ImmutableDictionaryLight<string, Action<TContextType, string>> elementMapDictionary;

        private IReadOnlyDictionary<string, Action<TContextType>> endElementMap;

        private ImmutableDictionaryLight<string, Action<TContextType>> endElementMapDictionary;

        private Action<TContextType, XmlTextReader, string> handleElementWildcardMappings;

        private bool useWildcardEndMapping;

        private IReadOnlyCollection<WildcardMapping<Action<TContextType>>> wildcardEndMappings;

        private IReadOnlyCollection<WildcardMapping<Action<TContextType, string>>> wildcardMappings;

        public XmlFileParser(
            IReadOnlyDictionary<string, Action<TContextType, string>> elementMap,
            IReadOnlyDictionary<string, Action<TContextType>> endElementMap = null,
            Func<string, XmlTextReader, bool> elementHandlerFunc = null)
        {
            this.endElementMap = endElementMap;
            this.elementHandlerFunc = elementHandlerFunc;

            this.CreateWildcardMappings(elementMap);
            this.CreateWildcardEndMappings(endElementMap);
        }

        public async Task<Result<TContextType>> ReadXmlFileAsync(string filename, TContextType item)
        {
            using (var dataStream = await GetDataStreamAsync(filename))
            {
                try
                {
                    var result = await this.ReadXmlFileAsync(dataStream, item);
                    return result.WithFilename(filename);
                }
                catch (Exception e)
                {
                    throw new ParseXmlException($"Error parsing file {filename}", filename, e);
                }
            }
        }

#pragma warning disable 1998
        public async Task<Result<TContextType>> ReadXmlFileAsync(
#pragma warning restore 1998
            Stream dataStream,
            TContextType contextItem)
        {
            this.ReadXmlStream(dataStream, (path, reader) => this.ElementHandlerFunc(contextItem, reader, path), path => this.EndOfElementHandlerFunc(contextItem, path));

            return new Result<TContextType>(null, ResultStatus.Success, contextItem);
        }

        private static void EmptyHandleElementWildcardMappings(TContextType arg1, XmlTextReader arg2, string arg3)
        {
        }

        private void CreateWildcardEndMappings(IReadOnlyDictionary<string, Action<TContextType>> endElementMap)
        {
            // get end element wildcard mappings
            this.wildcardEndMappings = this.wildcardMatcherContainer.GetWildcardMappings(endElementMap);

            // Remove wildcard mappings from normal map
            if (endElementMap != null && this.wildcardEndMappings.Count > 0)
            {
                endElementMap = endElementMap.Where(mapItem => !mapItem.Key.StartsWith("?")).ToDictionary();
            }

            this.endElementMap = endElementMap;

            this.useWildcardEndMapping = this.wildcardEndMappings.Count > 0;
            this.endElementMapDictionary = new ImmutableDictionaryLight<string, Action<TContextType>>(endElementMap);
        }

        private void CreateWildcardMappings(IReadOnlyDictionary<string, Action<TContextType, string>> elementMap)
        {
            // get start element wildcard mappings
            this.wildcardMappings = this.wildcardMatcherContainer.GetWildcardMappings(elementMap);

            // Remove wildcard mappings from normal map
            if (this.wildcardMappings.Count > 0)
            {
                elementMap = elementMap.Where(mapItem => !mapItem.Key.StartsWith("?")).ToDictionary();
                this.handleElementWildcardMappings = this.HandleElementWildcardMappings;
            }
            else
            {
                this.handleElementWildcardMappings = EmptyHandleElementWildcardMappings;
            }

            this.elementMapDictionary = new ImmutableDictionaryLight<string, Action<TContextType, string>>(elementMap);

            this.elementMap = elementMap;
        }

        private bool ElementHandlerFunc(TContextType contextItem, XmlTextReader reader, string path)
        {
            if (reader.IsEmptyElement == false)
            {
                // Is there an existing item in the element map for this path
                if (this.elementMap.TryGetValue(path, out var action))
                {
                    action(contextItem, reader.ReadTextContent());
                }
                else
                {
                    this.handleElementWildcardMappings(contextItem, reader, path);
                }
            }

            if (this.elementHandlerFunc != null)
            {
                return this.elementHandlerFunc(path, reader);
            }

            return true;
        }

        private bool EndOfElementHandlerFunc(TContextType contextItem, string path)
        {
            if (this.endElementMap != null)
            {
                if (this.endElementMap.TryGetValue(path, out var action))
                {
                    action(contextItem);
                }
                else
                {
                    this.HandleEndOfElementWildcardMapping(contextItem, path);
                }
            }

            return true;
        }

        private void HandleEndOfElementWildcardMapping(TContextType contextItem, string path)
        {
            if (!this.useWildcardEndMapping)
            {
                return;
            }

            if (this.nonEndMatches.Contains(path))
            {
                return;
            }

            foreach (var wildcardMapping in this.wildcardEndMappings)
            {
                if (wildcardMapping.WildcardMatcher.IsMatch(path))
                {
                    // add to element map
                    this.endElementMapDictionary.TryAdd(path, wildcardMapping.Action);
                    this.endElementMap = this.endElementMapDictionary.Value;

                    // execute the action
                    wildcardMapping.Action(contextItem);

                    return;
                }
            }

            this.nonEndMatches.Add(path);
        }

        private void HandleElementWildcardMappings(TContextType contextItem, XmlTextReader reader, string path)
        {
            if (this.nonMatches.Contains(path))
            {
                return;
            }

            foreach (var wildcardMapping in this.wildcardMappings)
            {
                if (wildcardMapping.WildcardMatcher.IsMatch(path))
                {
                    // add to the element map to treat this path as it was added manually
                    this.elementMapDictionary.TryAdd(path, wildcardMapping.Action);
                    this.elementMap = this.elementMapDictionary.Value;

                    // execute the action
                    wildcardMapping.Action(contextItem, reader.ReadTextContent());

                    return;
                }
            }

            this.nonMatches.Add(path);
        }
    }

    public class XmlFileParser
    {
        protected static async Task<Stream> GetDataStreamAsync(string filename)
        {
            Stream dataStream;

            using (var fileStream = File.OpenRead(filename))
            {
                var buffer = new byte[fileStream.Length];
                await fileStream.ReadAsync(buffer, 0, (int)fileStream.Length);

                dataStream = new MemoryStream(buffer);
            }

            return dataStream;
        }

        protected async Task ReadXmlFileAsync(string filename, Func<string, XmlTextReader, bool> elementHandlerFunc, Func<string, bool> endElementHandlerFunc = null)
        {
            using (var dataStream = await GetDataStreamAsync(filename))
            {
                this.ReadXmlStream(dataStream, elementHandlerFunc, endElementHandlerFunc);
            }
        }

        protected void ReadXmlStream(Stream dataStream, Func<string, XmlTextReader, bool> elementHandlerFunc, Func<string, bool> endElementHandlerFunc = null)
        {
            var pathList = new List<string>(16)
                               {
                                   string.Empty
                               };

            using (var xmlReader = new XmlTextReader(dataStream))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        var currentPath = pathList.LastItem() + "/" + xmlReader.Name;

                        if (xmlReader.IsEmptyElement == false)
                        {
                            pathList.Add(currentPath);
                        }

                        if (!elementHandlerFunc(currentPath, xmlReader))
                        {
                            break;
                        }

                        if (xmlReader.IsEmptyElement)
                        {
                            endElementHandlerFunc?.Invoke(currentPath);
                        }
                    }

                    if (xmlReader.NodeType == XmlNodeType.EndElement)
                    {
                        endElementHandlerFunc?.Invoke(pathList.LastItem());
                        pathList.RemoveLast();
                    }
                }
            }
        }
    }
}