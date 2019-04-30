namespace Serpent.Common.Xml
{
    using System.Collections.Generic;

    internal struct GetWildcardMappingResult<TActionType>
    {
        public GetWildcardMappingResult(IReadOnlyDictionary<string, TActionType> newElementMap, IReadOnlyCollection<WildcardMapping<TActionType>> wildcardMappings)
        {
            this.NewElementMap = newElementMap;
            this.WildcardMappings = wildcardMappings;
        }

        public IReadOnlyDictionary<string, TActionType> NewElementMap { get; }

        public IReadOnlyCollection<WildcardMapping<TActionType>> WildcardMappings { get; }
    }
}