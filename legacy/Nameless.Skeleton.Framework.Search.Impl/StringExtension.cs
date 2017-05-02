using System.Net;

namespace Nameless.Skeleton.Framework.Search {

    internal static class StringExtension {

        #region Internal Static Methods

        internal static string RemoveHtmlTags(this string source, bool htmlDecode = false) {
            if (string.IsNullOrEmpty(source)) { return string.Empty; }

            var content = new char[source.Length];

            var cursor = 0;
            var inside = false;
            for (var idx = 0; idx < source.Length; idx++) {
                char current = source[idx];

                switch (current) {
                    case '<':
                        inside = true;
                        continue;
                    case '>':
                        inside = false;
                        continue;
                }

                if (!inside) {
                    content[cursor++] = current;
                }
            }

            var result = new string(content, 0, cursor);
            if (htmlDecode) {
                result = WebUtility.HtmlDecode(result);
            }

            return result;
        }

        #endregion Internal Static Methods
    }
}