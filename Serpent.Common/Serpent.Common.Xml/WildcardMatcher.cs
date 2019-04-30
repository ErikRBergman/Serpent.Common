namespace Serpent.Common.Xml
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    internal class WildcardMatcher
    {
        private readonly object lockObject = new object();

        private readonly Regex regex;

        private HashSet<string> matches = new HashSet<string>();

        private HashSet<string> nonMatches = new HashSet<string>();

        public WildcardMatcher(Regex regex)
        {
            this.regex = regex;
        }

        public IEnumerable<string> Matches => this.matches;

        public IEnumerable<string> NonMatches => this.nonMatches;

        public bool IsMatch(string input)
        {
            if (this.matches.Contains(input))
            {
                return true;
            }

            if (this.nonMatches.Contains(input))
            {
                return false;
            }

            lock (this.lockObject)
            {
                var isMatch = this.regex.IsMatch(input);

                if (isMatch)
                {
                    var newMatches = new HashSet<string>(this.matches)
                                         {
                                             input
                                         };

                    this.matches = newMatches;
                }
                else
                {
                    var newNonMatches = new HashSet<string>(this.nonMatches)
                                            {
                                                input
                                            };

                    this.nonMatches = newNonMatches;
                }

                return isMatch;
            }
        }
    }
}