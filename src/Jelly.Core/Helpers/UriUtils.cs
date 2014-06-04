using System;
using System.Collections.Specialized;
using System.Text;
using Jelly.Extensions;

namespace Jelly.Helpers
{
    /// <summary>
    /// Uri Helper class.
    /// </summary>
    public static class UriUtils
    {
        public static string BuildQueryString(NameValueCollection queryStrings)
        {
            StringBuilder builder = new StringBuilder();

            if (queryStrings != null)
            {
                foreach (string key in queryStrings.Keys)
                {
                    builder.AppendFormat("{0}={1}&", key, queryStrings[key]);
                }
            }

            string qs = builder.ToString();
            if (qs.EndsWith("&", StringComparison.OrdinalIgnoreCase))
            {
                qs = qs.Remove(qs.Length - 1);
            }

            if (!String.IsNullOrWhiteSpace(qs))
            {
                qs = string.Concat("?", qs);
            }

            return qs;
        }

        /// <summary>
        /// Append url a query delimiter.
        /// </summary>
        public static string AppendQueryDelimiter(string url)
        {
            ExceptionManager.ThrowArgumentNullExceptionIfNullOrEmpty(url, "url");

            if (url.Contains("?"))
            {
                if (!url.IsLastWord('?') && !url.IsLastWord('&'))
                {
                    url += "&";
                }
            }
            else
            {
                url += "?";
            }

            return url;
        }
    }
}
