namespace System.CommandLine.Readme.Tests
{
    using System.CommandLine;
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    public class CommandFormatHelperTests
    {
        public class FormatAliases
        {
            [Fact]
            public void ReturnsFormattedAliasesAsInlineCode()
            {
                var aliases = new[] { "-h", "--help" };
                var result = CommandFormatHelper.FormatAliases(aliases);
                result.Should().Be("`-h, --help`");
            }

            [Fact]
            public void ReturnsEmptyStringWhenNoAliases()
            {
                var aliases = new string[] { };
                var result = CommandFormatHelper.FormatAliases(aliases);
                result.Should().BeEmpty();
            }

            [Fact]
            public void HandlesSingleAlias()
            {
                var aliases = new[] { "-v" };
                var result = CommandFormatHelper.FormatAliases(aliases);
                result.Should().Be("`-v`");
            }

            [Fact]
            public void HandlesMultipleAliases()
            {
                var aliases = new[] { "-o", "--output", "-out" };
                var result = CommandFormatHelper.FormatAliases(aliases);
                result.Should().Be("`-o, --output, -out`");
            }
        }

        public class FormatCommands
        {
            [Fact]
            public void ReturnsEmptyStringWhenNoCommands()
            {
                var commands = new Command[] { };
                var result = CommandFormatHelper.FormatCommands(commands, 2);
                result.Should().BeEmpty();
            }

            [Fact]
            public void FormatsCommandsWithDescription()
            {
                var command = new Command("test", "Test command");
                var commands = new[] { command };
                var result = CommandFormatHelper.FormatCommands(commands, 2);
                result.Should().Contain("**test**");
                result.Should().Contain("*Test command*");
            }

            [Fact]
            public void FormatsMultipleCommands()
            {
                var command1 = new Command("cmd1", "First command");
                var command2 = new Command("cmd2", "Second command");
                var commands = new[] { command1, command2 };
                var result = CommandFormatHelper.FormatCommands(commands, 2);
                result.Should().Contain("**cmd1**");
                result.Should().Contain("**cmd2**");
            }

            [Fact]
            public void FormatsCommandsWithAliases()
            {
                var command = new Command("test", "Test command");
                command.AddAlias("t");
                var commands = new[] { command };
                var result = CommandFormatHelper.FormatCommands(commands, 2);
                result.Should().Contain("Aliases:");
            }
        }

        public class FormatRootCommand
        {
            [Fact]
            public void FormatsRootCommandWithNameAndDescription()
            {
                var rootCommand = new RootCommand("Root description");
                var result = CommandFormatHelper.FormatRootCommand(rootCommand);
                result.Should().Contain($"**{rootCommand.Name}**");
                result.Should().Contain("*Root description*");
                result.Should().Contain("---");
            }

            [Fact]
            public void IncludesOptionsWhenPresent()
            {
                var rootCommand = new RootCommand();
                var option = new Option<string>("--test", "Test option");
                rootCommand.Add(option);
                var result = CommandFormatHelper.FormatRootCommand(rootCommand);
                result.Should().Contain("options");
                result.Should().Contain("--test");
            }

            [Fact]
            public void IncludesArgumentsWhenPresent()
            {
                var rootCommand = new RootCommand();
                var argument = new Argument<string>("input");
                argument.Description = "Input file";
                rootCommand.Add(argument);
                var result = CommandFormatHelper.FormatRootCommand(rootCommand);
                result.Should().Contain("arguments");
                result.Should().Contain("input");
            }

            [Fact]
            public void IncludesSubcommandsWhenPresent()
            {
                var rootCommand = new RootCommand();
                var subcommand = new Command("sub", "Subcommand");
                rootCommand.Add(subcommand);
                var result = CommandFormatHelper.FormatRootCommand(rootCommand);
                result.Should().Contain("subcommands");
                result.Should().Contain("**sub**");
            }

            [Fact]
            public void HandlesRootCommandWithoutDescription()
            {
                var rootCommand = new RootCommand();
                var result = CommandFormatHelper.FormatRootCommand(rootCommand);
                result.Should().Contain($"**{rootCommand.Name}**");
            }
        }

        public class FormatCommand
        {
            [Fact]
            public void FormatsCommandWithNameAndDescription()
            {
                var command = new Command("build", "Build the project");
                var result = CommandFormatHelper.FormatCommand(command, 2);
                result.Should().Contain("**build**");
                result.Should().Contain("*Build the project*");
            }

            [Fact]
            public void IncludesAliasesInFormatting()
            {
                var command = new Command("build", "Build the project");
                command.AddAlias("b");
                command.AddAlias("compile");
                var result = CommandFormatHelper.FormatCommand(command, 2);
                result.Should().Contain("Aliases:");
                result.Should().Contain("`build, b, compile`");
            }

            [Fact]
            public void IncludesOptionsWhenPresent()
            {
                var command = new Command("build", "Build the project");
                var option = new Option<bool>("--verbose", "Verbose output");
                command.Add(option);
                var result = CommandFormatHelper.FormatCommand(command, 2);
                result.Should().Contain("--verbose");
                result.Should().Contain("*Verbose output*");
            }

            [Fact]
            public void IncludesArgumentsWhenPresent()
            {
                var command = new Command("build", "Build the project");
                var argument = new Argument<string>("project");
                argument.Description = "Project file";
                command.Add(argument);
                var result = CommandFormatHelper.FormatCommand(command, 2);
                result.Should().Contain("arguments");
                result.Should().Contain("project");
            }

            [Fact]
            public void IncludesNestedSubcommandsWhenPresent()
            {
                var command = new Command("build", "Build the project");
                var subcommand = new Command("clean", "Clean output");
                command.Add(subcommand);
                var result = CommandFormatHelper.FormatCommand(command, 2);
                result.Should().Contain("subcommands");
                result.Should().Contain("**clean**");
            }

            [Fact]
            public void HandlesCommandWithoutDescription()
            {
                var command = new Command("test");
                var result = CommandFormatHelper.FormatCommand(command, 2);
                result.Should().Contain("**test**");
            }

            [Fact]
            public void AppliesCorrectIndentationBasedOnLevel()
            {
                var command = new Command("test", "Test");
                var resultLevel2 = CommandFormatHelper.FormatCommand(command, 2);
                var resultLevel3 = CommandFormatHelper.FormatCommand(command, 3);
                resultLevel2.Should().NotBeEmpty();
                resultLevel3.Should().NotBeEmpty();
            }
        }

        public class FormatOptions
        {
            [Fact]
            public void ReturnsEmptyStringWhenNoOptions()
            {
                var options = new Option[] { };
                var result = CommandFormatHelper.FormatOptions(options, 2);
                result.Should().BeEmpty();
            }

            [Fact]
            public void FormatsOptionWithNameAndDescription()
            {
                var option = new Option<string>("--output", "Output file");
                var options = new[] { option };
                var result = CommandFormatHelper.FormatOptions(options, 2);
                result.Should().Contain("**output**");
                result.Should().Contain("*Output file*");
            }

            [Fact]
            public void IncludesAliases()
            {
                var option = new Option<string>("--output", "Output file");
                option.AddAlias("-o");
                var options = new[] { option };
                var result = CommandFormatHelper.FormatOptions(options, 2);
                result.Should().Contain("Aliases:");
                result.Should().Contain("`--output, -o`");
            }

            [Fact]
            public void ShowsRequiredFlagWhenOptionIsRequired()
            {
                var option = new Option<string>("--required")
                {
                    IsRequired = true
                };
                var options = new[] { option };
                var result = CommandFormatHelper.FormatOptions(options, 2);
                result.Should().Contain("***IsRequired;***");
            }

            [Fact]
            public void DoesNotShowRequiredFlagWhenOptionIsNotRequired()
            {
                var option = new Option<string>("--optional")
                {
                    IsRequired = false
                };
                var options = new[] { option };
                var result = CommandFormatHelper.FormatOptions(options, 2);
                result.Should().NotContain("IsRequired");
            }

            [Fact]
            public void IncludesValueType()
            {
                var option = new Option<int>("--count");
                var options = new[] { option };
                var result = CommandFormatHelper.FormatOptions(options, 2);
                result.Should().Contain("ValueType=");
                result.Should().Contain("Int32");
            }

            [Fact]
            public void FormatsMultipleOptions()
            {
                var option1 = new Option<string>("--output");
                var option2 = new Option<bool>("--verbose");
                var options = new Option[] { option1, option2 };
                var result = CommandFormatHelper.FormatOptions(options, 2);
                result.Should().Contain("--output");
                result.Should().Contain("--verbose");
            }
        }

        public class FormatArgument
        {
            [Fact]
            public void ReturnsEmptyStringWhenNoArguments()
            {
                var arguments = new Argument[] { };
                var result = CommandFormatHelper.FormatArgument(arguments, 2);
                result.Should().BeEmpty();
            }

            [Fact]
            public void FormatsArgumentWithNameAndDescription()
            {
                var argument = new Argument<string>("file");
                argument.Description = "File to process";
                var arguments = new[] { argument };
                var result = CommandFormatHelper.FormatArgument(arguments, 2);
                result.Should().Contain("**file**");
                result.Should().Contain("*File to process*");
            }

            [Fact]
            public void IncludesValueType()
            {
                var argument = new Argument<int>("count");
                var arguments = new[] { argument };
                var result = CommandFormatHelper.FormatArgument(arguments, 2);
                result.Should().Contain("ValueType=");
                result.Should().Contain("Int32");
            }

            [Fact]
            public void FormatsMultipleArguments()
            {
                var argument1 = new Argument<string>("source");
                var argument2 = new Argument<string>("destination");
                var arguments = new[] { argument1, argument2 };
                var result = CommandFormatHelper.FormatArgument(arguments, 2);
                result.Should().Contain("source");
                result.Should().Contain("destination");
            }
        }

        public class FormatSymbols
        {
            [Fact]
            public void ReturnsEmptyStringWhenNoSymbols()
            {
                var symbols = new Symbol[] { };
                var result = CommandFormatHelper.FormatSymbols(symbols, 2);
                result.Should().BeEmpty();
            }

            [Fact]
            public void FormatsSymbolWithNameAndDescription()
            {
                var option = new Option<string>("--test", "Test option");
                var symbols = new Symbol[] { option };
                var result = CommandFormatHelper.FormatSymbols(symbols, 2);
                result.Should().Contain("test");
                result.Should().Contain("Test option");
            }

            [Fact]
            public void FormatsMultipleSymbols()
            {
                var option1 = new Option<string>("--opt1", "First option");
                var option2 = new Option<string>("--opt2", "Second option");
                var symbols = new Symbol[] { option1, option2 };
                var result = CommandFormatHelper.FormatSymbols(symbols, 2);
                result.Should().Contain("opt1");
                result.Should().Contain("opt2");
            }

            [Fact]
            public void HandlesSymbolsWithoutDescription()
            {
                var option = new Option<string>("--test");
                var symbols = new Symbol[] { option };
                var result = CommandFormatHelper.FormatSymbols(symbols, 2);
                result.Should().Contain("test");
            }
        }
    }
}

