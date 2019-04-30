namespace Serpent.Common.Xml
{
    internal class WildcardMapping<TActionType>
    {
        public WildcardMapping(string pattern, WildcardMatcher wildcardMatcher, TActionType action)
        {
            this.Pattern = pattern;
            this.WildcardMatcher = wildcardMatcher;
            this.Action = action;
        }

        public TActionType Action { get; }

        public string Pattern { get; }

        public WildcardMatcher WildcardMatcher { get; }
    }
}