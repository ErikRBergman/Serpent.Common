namespace Serpent.Common.Xml
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal class WildcardMatcherContainer
    {
        private readonly ConcurrentDictionary<string, WildcardMatcher> wildcardMatchItems = new ConcurrentDictionary<string, WildcardMatcher>();

        public IReadOnlyCollection<WildcardMapping<TActionType>> GetWildcardMappings<TActionType>(IReadOnlyDictionary<string, TActionType> elementMap)
        {
            var elementWildcardMappings = elementMap.Where(mapItem => mapItem.Key.StartsWith("?")).ToArray();

            var wildcardMappings = new List<WildcardMapping<TActionType>>();

            foreach (var wildcardMapping in elementWildcardMappings)
            {
                var regex = this.wildcardMatchItems.GetOrAdd(wildcardMapping.Key, k => new WildcardMatcher(new Regex(k.Substring(1), RegexOptions.Compiled)));
                wildcardMappings.Add(new WildcardMapping<TActionType>(wildcardMapping.Key, regex, wildcardMapping.Value));
            }

            return wildcardMappings;
        }
    }
}