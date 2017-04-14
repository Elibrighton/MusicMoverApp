using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class RegexHelper
    {
        public static string GetRegexMatchValue(string text, string pattern)
        {
            var match = string.Empty;
            var r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(text);

            if (m.Success)
            {
                match = m.Value;
            }

            return match;
        }

        public static List<string> GetMatchesList(string text, string pattern)
        {
            MatchCollection matchList = Regex.Matches(text, pattern);
            return matchList.Cast<Match>().Select(match => match.Value).ToList();
        }
    }
}
