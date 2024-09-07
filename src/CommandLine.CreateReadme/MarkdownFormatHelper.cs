// Ignore Spelling: Readme Inline Blockquote

namespace System.CommandLine.Readme
{
    using System.Collections.Generic;
    using System.Text;

    internal static class MarkdownFormatHelper
    {
        public static string AsHeading(this string text, int level)
        {
            if (text == null || (level < 0 && level > 6))
            {
                return string.Empty;
            }

            return $"{new string('#', level)} {text}\n\n";
        }

        public static string AddLineBreak(this string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return $"{text}\n\n";
        }

        public static string AsParagraph(this string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return $"{text}\n\n";
        }

        public static string AsBold(this string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return $"**{text}**";
        }

        public static string AsItalic(this string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return $"*{text}*";
        }

        public static string AsBoldAndItalic(this string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return $"***{text}***";
        }

        public static string AsCodeBlock(this string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return $"```{text}```";
        }

        public static string AsCodeBlock(this string text, string type)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return $"```{type}\n{text}\n```";
        }

        public static string AsCodeInline(this string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return $"`{text}`";
        }

        public static string AsBlockquote(this string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return $"> {text}";
        }

        public static string AsNestedBlockquote(this string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return $">> {text}";
        }

        public static string AsLink(this string text, string url)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return $"[{text}]({url})";
        }

        public static string AsOrderedList(this IEnumerable<string> items)
        {
            if (items == null)
            {
                return string.Empty;
            }

            var orderedList = new StringBuilder();
            var index = 1;
            foreach (var item in items)
            {
                orderedList.Append($"{index}. {item}\n");
                index++;
            }

            return orderedList.ToString();
        }

        public static string AsOrderedListItem(this string item, int index)
        {
            if (item == null || index < 0)
            {
                return string.Empty;
            }

            return $"{index}. {item}\n";
        }

        public static string AsUnorderedList(this IEnumerable<string> items)
        {
            if (items == null)
            {
                return string.Empty;
            }

            var unorderedList = new StringBuilder();
            foreach (var item in items)
            {
                unorderedList.Append($"- {item}\n");
            }

            return unorderedList.ToString();
        }

        public static string AsUnorderedListItem(this string item)
        {
            if (item == null)
            {
                return string.Empty;
            }

            return $"- {item}\n";
        }

        public static string AsTable(this IEnumerable<IEnumerable<string>> rows)
        {
            if (rows == null)
            {
                return string.Empty;
            }

            var table = new StringBuilder();
            foreach (var row in rows)
            {
                table.Append('|');
                foreach (var cell in row)
                {
                    table.Append($"{cell}|");
                }

                table.Append('\n');
            }

            return table.ToString();
        }

        public static string AsTableHeader(this IEnumerable<string> headers)
        {
            if (headers == null)
            {
                return string.Empty;
            }

            var tableHeader = new StringBuilder();
            tableHeader.Append('|');
            foreach (var header in headers)
            {
                tableHeader.Append($"{header}|");
            }

            tableHeader.Append('\n');
            tableHeader.Append('|');
            foreach (var header in headers)
            {
                tableHeader.Append("---|");
            }

            tableHeader.Append('\n');

            return tableHeader.ToString();
        }

        public static string AsNestedListItem(this string item)
        {
            if (item == null)
            {
                return string.Empty;
            }

            return $"* {item}";
        }

        public static string AddTab(this string text, int count = 1)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return $"{new string('\t', count)}{text}";
        }

        public static string AddHorizontalRule()
        {
            return $"---\n";
        }
    }
}
