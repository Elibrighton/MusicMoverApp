using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class StringHelper
    {
        public static string GetSpanClassValue(this string response, string name)
        {
            if (string.IsNullOrEmpty(response)) throw new ArgumentNullException(string.Concat(name, " class is null"));

            var value = string.Empty;
            var openTagPattern = string.Concat("<span\\sclass=\"", name, "\">");
            var innterPattern = "[^<]*";
            var closeTagPattern = "</span>";

            var pattern = string.Concat(openTagPattern, innterPattern, closeTagPattern);

            var matchValue = RegexHelper.GetRegexMatchValue(response, pattern);

            if (!string.IsNullOrEmpty(matchValue))
            {
                openTagPattern = openTagPattern.Replace("\\s", " ");
                value = matchValue.Replace(openTagPattern, "").Replace(closeTagPattern, "");
            }

            return value;
        }
        public static string GetSpanItemPropValue(this string response, string name)
        {
            if (string.IsNullOrEmpty(response)) throw new ArgumentNullException(string.Concat(name, " item prop is null"));

            var value = string.Empty;
            var openTagPattern = string.Concat("<span\\sitemprop=\"", name, "\">");
            var innterPattern = "[^<]*";
            var closeTagPattern = "</span>";

            var pattern = string.Concat(openTagPattern, innterPattern, closeTagPattern);

            var matchValue = RegexHelper.GetRegexMatchValue(response, pattern);

            if (!string.IsNullOrEmpty(matchValue))
            {
                openTagPattern = openTagPattern.Replace("\\s", " ");
                value = matchValue.Replace(openTagPattern, "").Replace(closeTagPattern, "");
            }

            return value;
        }

        public static string GetParagraphClassValue(this string response, string name)
        {
            if (string.IsNullOrEmpty(response)) throw new ArgumentNullException(string.Concat(name, " class is null"));

            var value = string.Empty;
            var openTagPattern = string.Concat("<p\\sclass=\"", name, "\">");
            var innterPattern = "[^<]*";
            var closeTagPattern = "</p>";

            var pattern = string.Concat(openTagPattern, innterPattern, closeTagPattern);

            var matchValue = RegexHelper.GetRegexMatchValue(response, pattern);

            if (!string.IsNullOrEmpty(matchValue))
            {
                openTagPattern = openTagPattern.Replace("\\s", " ");
                value = matchValue.Replace(openTagPattern, "").Replace(closeTagPattern, "");
            }

            return value;
        }

        public static string GetLiValue(this string response, string name)
        {
            if (string.IsNullOrEmpty(response)) throw new ArgumentNullException(string.Concat(name, " class is null"));

            var value = string.Empty;
            var openTagPattern = string.Concat("<li>", name, "<span>");
            var innterPattern = "[^<]*";
            var closeTagPattern = "</span></li>";

            var pattern = string.Concat(openTagPattern, innterPattern, closeTagPattern);

            var matchValue = RegexHelper.GetRegexMatchValue(response, pattern);

            if (!string.IsNullOrEmpty(matchValue))
            {
                openTagPattern = openTagPattern.Replace("\\s", " ");
                value = matchValue.Replace(openTagPattern, "").Replace(closeTagPattern, "");
            }

            return value;
        }

        public static string GetDivIdValue(this string response, string pattern)
        {
            if (string.IsNullOrEmpty(pattern)) throw new ArgumentNullException("Pattern is null");

            var value = string.Empty;
            
            var matchValue = RegexHelper.GetRegexMatchValue(response, pattern);

            if (!string.IsNullOrEmpty(matchValue))
            {
                value = matchValue;
            }

            return value;
        }
    }
}
