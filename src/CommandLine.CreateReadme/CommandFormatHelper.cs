// Ignore Spelling: Readme

namespace System.CommandLine.Readme
{
    using System.Collections.Generic;
    using System.CommandLine;
    using System.Linq;
    using System.Text;

    internal static class CommandFormatHelper
    {
        private static readonly int RootLevel = 2;

        public static string FormatAliases(IReadOnlyCollection<string> aliases)
        {
            if (aliases.Count == 0)
            {
                return string.Empty;
            }

            var text = new StringBuilder();
            text.Append(string.Join(", ", aliases.ToArray()).AsCodeInline());
            return text.ToString();
        }

        public static string FormatCommands(IReadOnlyCollection<Command> items, int level)
        {
            if (items.Count == 0)
            {
                return string.Empty;
            }

            var readme = new StringBuilder();
            foreach (var command in items)
            {
                readme.Append(FormatCommand(command, level));
            }

            return readme.ToString();
        }

        public static string FormatRootCommand(RootCommand item)
        {
            int level = RootLevel;
            var readme = new StringBuilder();
            readme.AppendLine(MarkdownFormatHelper.AddHorizontalRule());
            readme.Append($"{item.Name.AsBold()}: {item.Description?.AsItalic()}"
                .AsHeading(level));

            if (item.Options.Any())
            {
                readme.Append($"{item.Name.AsBold()} options"
                    .AsHeading(level));
                readme.Append(FormatOptions(item.Options, level));
            }

            if (item.Arguments.Any())
            {
                readme.Append($"{item.Name.AsBold()} arguments:"
                    .AsHeading(level));
                readme.Append(FormatSymbols(item.Arguments, level + 1));
            }

            if (item.Subcommands.Any())
            {
                readme.Append($"{item.Name.AsBold()} subcommands"
                    .AsHeading(level));
                readme.Append(FormatCommands(item.Subcommands, level + 1));
            }

            return readme.ToString();
        }

        public static string FormatCommand(Command item, int level)
        {
            var tabs = level - RootLevel;
            var readme = new StringBuilder();
            readme.Append($"{item.Name.AsBold()}: {item.Description?.AsItalic()}"
                .AsHeading(level)
                .AsNestedListItem());
            readme.AppendLine($"Aliases: {FormatAliases(item.Aliases)}\n".AddTab());

            if (item.Options.Any())
            {
                readme.Append(FormatOptions(item.Options, level));
            }

            if (item.Arguments.Any())
            {
                readme.Append($"{item.Name.AsBold()} arguments:"
                    .AsHeading(level)
                    .AsNestedListItem()
                    .AddTab(tabs));
                readme.Append(FormatSymbols(item.Arguments, level + 1));
            }

            if (item.Subcommands.Any())
            {
                readme.Append($"{item.Name.AsBold()} subcommands"
                    .AsHeading(level)
                    .AsNestedListItem()
                    .AddTab(tabs));
                readme.Append(FormatCommands(item.Subcommands, level + 1));
            }

            return readme.ToString();
        }

        public static string FormatOptions(IReadOnlyCollection<Option> items, int level)
        {
            if (items.Count == 0)
            {
                return string.Empty;
            }

            var tabs = level - RootLevel;
            var readme = new StringBuilder();
            foreach (var option in items)
            {
                readme.Append($"{option.Name.AsBold()}: {option.Description?.AsItalic()}"
                    .AsHeading(level)
                    .AsNestedListItem()
                    .AddTab(tabs));
                readme.AppendLine($"Aliases: {FormatAliases(option.Aliases)}\n"
                    .AddTab(tabs + 1));
                if (option.IsRequired)
                {
                    readme.AppendLine($"IsRequired;"
                        .AsBoldAndItalic()
                        .AddTab(tabs + 1));
                }

                readme.AppendLine($"ValueType={option.ValueType?.ToString().AsItalic()}\n"
                    .AddTab(tabs + 1));
            }

            return readme.ToString();
        }

        public static string FormatArgument(IReadOnlyCollection<Argument> items, int level)
        {
            if (items.Count == 0)
            {
                return string.Empty;
            }

            var tabs = level - RootLevel;
            var readme = new StringBuilder();
            foreach (var option in items)
            {
                readme.Append($"{option.Name.AsBold()} : {option.Description?.AsItalic()}"
                    .AsHeading(level)
                    .AddTab(tabs));
                readme.Append($"ValueType={option.ValueType.ToString().AsItalic()}"
                    .AsBlockquote()
                    .AddTab(tabs));
            }

            return readme.ToString();
        }

        public static string FormatSymbols(IEnumerable<Symbol> items, int level)
        {
            if (!items.Any())
            {
                return string.Empty;
            }

            var tabs = level - RootLevel;
            var readme = new StringBuilder();
            foreach (var symbol in items)
            {
                readme.Append($"{symbol.Name} : {symbol.Description}"
                    .AsHeading(level)
                    .AddTab(tabs));
            }

            return readme.ToString();
        }
    }
}
