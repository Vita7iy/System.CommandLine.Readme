namespace System.CommandLine.Readme.Tests
{
    using System.CommandLine;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;

    public class CreateMarkDownTests
    {
        public class CreateCommandLineReadmeFile
        {
            [Fact]
            public async void CreatesReadmeFileAtGivenPath()
            {
                var rootCommand = new RootCommand("Test");
                rootCommand.AddCommandLineReadmeToRoot();
                var tempFile = Path.GetTempFileName();
                try
                {
                    var args = new[] { "readme", "--readme-file", tempFile };
                    await rootCommand.InvokeAsync(args);
                    File.Exists(tempFile).Should().BeTrue();
                    var content = File.ReadAllText(tempFile);
                    content.Should().Contain($"# {rootCommand.Name}");
                }
                finally
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }
        }
        public class CreateReadme
        {
            [Fact]
            public void CreatesReadmeWithRootCommandName()
            {
                var rootCommand = new RootCommand("Test application");
                var result = CreateMarkDown.CreateReadme(rootCommand);
                result.Should().Contain($"# {rootCommand.Name}");
            }

            [Fact]
            public void CreatesReadmeWithDescription()
            {
                var rootCommand = new RootCommand("Test description");
                var result = CreateMarkDown.CreateReadme(rootCommand);
                result.Should().Contain("*Test description*");
            }

            [Fact]
            public void IncludesHorizontalRule()
            {
                var rootCommand = new RootCommand();
                var result = CreateMarkDown.CreateReadme(rootCommand);
                result.Should().Contain("---");
            }

            [Fact]
            public void IncludesOptionsInReadme()
            {
                var rootCommand = new RootCommand();
                var option = new Option<string>("--config", "Config file");
                rootCommand.Add(option);
                var result = CreateMarkDown.CreateReadme(rootCommand);
                result.Should().Contain("--config");
                result.Should().Contain("*Config file*");
            }

            [Fact]
            public void IncludesSubcommandsInReadme()
            {
                var rootCommand = new RootCommand();
                var subcommand = new Command("build", "Build command");
                rootCommand.Add(subcommand);
                var result = CreateMarkDown.CreateReadme(rootCommand);
                result.Should().Contain("**build**");
                result.Should().Contain("*Build command*");
            }

            [Fact]
            public void IncludesArgumentsInReadme()
            {
                var rootCommand = new RootCommand();
                var argument = new Argument<string>("path");
                argument.Description = "Path to file";
                rootCommand.Add(argument);
                var result = CreateMarkDown.CreateReadme(rootCommand);
                result.Should().Contain("path");
            }

            [Fact]
            public void HandlesComplexCommandStructure()
            {
                var rootCommand = new RootCommand("Main app");
                
                var option = new Option<bool>("--verbose", "Verbose output");
                rootCommand.Add(option);
                
                var subcommand = new Command("process", "Process data");
                var subOption = new Option<string>("--input", "Input file");
                subcommand.Add(subOption);
                rootCommand.Add(subcommand);
                
                var result = CreateMarkDown.CreateReadme(rootCommand);
                
                result.Should().Contain($"# {rootCommand.Name}");
                result.Should().Contain("--verbose");
                result.Should().Contain("**process**");
                result.Should().Contain("--input");
            }

            [Fact]
            public void ReturnsNonEmptyStringForMinimalRootCommand()
            {
                var rootCommand = new RootCommand();
                var result = CreateMarkDown.CreateReadme(rootCommand);
                result.Should().NotBeEmpty();
                result.Should().Contain($"# {rootCommand.Name}");
            }
        }

        public class CreateReadmeFile
        {
            [Fact]
            public void CreatesFileAtSpecifiedPath()
            {
                var tempFile = Path.GetTempFileName();
                try
                {
                    var rootCommand = new RootCommand("Test");
                    
                    CreateMarkDown.CreateReadmeFile(rootCommand, tempFile);
                    
                    File.Exists(tempFile).Should().BeTrue();
                    var content = File.ReadAllText(tempFile);
                    content.Should().Contain($"# {rootCommand.Name}");
                }
                finally
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }

            [Fact]
            public void WritesCompleteMarkdownContent()
            {
                var tempFile = Path.GetTempFileName();
                try
                {
                    var rootCommand = new RootCommand("Test application");
                    var option = new Option<string>("--output", "Output file");
                    rootCommand.Add(option);
                    
                    CreateMarkDown.CreateReadmeFile(rootCommand, tempFile);
                    
                    var content = File.ReadAllText(tempFile);
                    content.Should().Contain($"# {rootCommand.Name}");
                    content.Should().Contain("*Test application*");
                    content.Should().Contain("--output");
                }
                finally
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }

            [Fact]
            public void OverwritesExistingFile()
            {
                var tempFile = Path.GetTempFileName();
                try
                {
                    File.WriteAllText(tempFile, "Old content");
                    
                    var rootCommand = new RootCommand("New content");
                    
                    CreateMarkDown.CreateReadmeFile(rootCommand, tempFile);
                    
                    var content = File.ReadAllText(tempFile);
                    content.Should().NotContain("Old content");
                    content.Should().Contain($"# {rootCommand.Name}");
                }
                finally
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }

            [Fact]
            public void CreatesFileInNonExistingDirectory()
            {
                var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                var tempFile = Path.Combine(tempDir, "README.md");
                
                try
                {
                    Directory.CreateDirectory(tempDir);
                    
                    var rootCommand = new RootCommand();
                    
                    CreateMarkDown.CreateReadmeFile(rootCommand, tempFile);
                    
                    File.Exists(tempFile).Should().BeTrue();
                }
                finally
                {
                    if (Directory.Exists(tempDir))
                    {
                        Directory.Delete(tempDir, true);
                    }
                }
            }
        }

        public class AddCommandLineReadmeToRoot
        {
            [Fact]
            public void AddsReadmeCommandToRootCommand()
            {
                var rootCommand = new RootCommand();
                rootCommand.AddCommandLineReadmeToRoot();
                
                rootCommand.Subcommands.Should().Contain(c => c.Name == "readme");
            }

            [Fact]
            public void ReadmeCommandHasCorrectDescription()
            {
                var rootCommand = new RootCommand();
                rootCommand.AddCommandLineReadmeToRoot();
                
                var readmeCommand = rootCommand.Subcommands.First(c => c.Name == "readme");
                readmeCommand.Description.Should().Be("Create the 'Readme' Markdown file.");
            }

            [Fact]
            public void ReadmeCommandHasRmAlias()
            {
                var rootCommand = new RootCommand();
                rootCommand.AddCommandLineReadmeToRoot();
                
                var readmeCommand = rootCommand.Subcommands.First(c => c.Name == "readme");
                readmeCommand.Aliases.Should().Contain("rm");
            }

            [Fact]
            public void ReadmeCommandHasReadmeFileOption()
            {
                var rootCommand = new RootCommand();
                rootCommand.AddCommandLineReadmeToRoot();
                
                var readmeCommand = rootCommand.Subcommands.First(c => c.Name == "readme");
                readmeCommand.Options.Should().Contain(o => o.Name == "readme-file");
            }

            [Fact]
            public void ReadmeFileOptionIsRequired()
            {
                var rootCommand = new RootCommand();
                rootCommand.AddCommandLineReadmeToRoot();
                
                var readmeCommand = rootCommand.Subcommands.First(c => c.Name == "readme");
                var option = readmeCommand.Options.FirstOrDefault(o => o.Name == "readme-file");
                option.IsRequired.Should().BeTrue();
            }

            [Fact]
            public void ReadmeFileOptionHasMdAlias()
            {
                var rootCommand = new RootCommand();
                rootCommand.AddCommandLineReadmeToRoot();
                
                var readmeCommand = rootCommand.Subcommands.First(c => c.Name == "readme");
                var option = readmeCommand.Options.First(o => o.Name == "readme-file");
                option.Aliases.Should().Contain("-md");
            }

            [Fact]
            public void ReadmeFileOptionHasDescription()
            {
                var rootCommand = new RootCommand();
                rootCommand.AddCommandLineReadmeToRoot();
                
                var readmeCommand = rootCommand.Subcommands.First(c => c.Name == "readme");
                var option = readmeCommand.Options.First(o => o.Name == "readme-file");
                option.Description.Should().Be("The name of the ReadMe file.");
            }

            [Fact]
            public async void ReadmeCommandCreatesFileWhenInvoked()
            {
                var tempFile = Path.GetTempFileName();
                try
                {
                    var rootCommand = new RootCommand("My Application");
                    rootCommand.AddCommandLineReadmeToRoot();
                    
                    var args = new[] { "readme", "--readme-file", tempFile };
                    await rootCommand.InvokeAsync(args);
                    
                    File.Exists(tempFile).Should().BeTrue();
                    var content = File.ReadAllText(tempFile);
                    content.Should().Contain($"# {rootCommand.Name}");
                }
                finally
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }

            [Fact]
            public async void CanUseRmAliasToCreateReadme()
            {
                var tempFile = Path.GetTempFileName();
                try
                {
                    var rootCommand = new RootCommand();
                    rootCommand.AddCommandLineReadmeToRoot();
                    
                    var args = new[] { "rm", "-md", tempFile };
                    await rootCommand.InvokeAsync(args);
                    
                    File.Exists(tempFile).Should().BeTrue();
                }
                finally
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }

            [Fact]
            public async void GeneratesCorrectReadmeContentWhenInvoked()
            {
                var tempFile = Path.GetTempFileName();
                try
                {
                    var rootCommand = new RootCommand("Test description");
                    var option = new Option<string>("--config", "Config file");
                    rootCommand.Add(option);
                    rootCommand.AddCommandLineReadmeToRoot();
                    
                    var args = new[] { "readme", "--readme-file", tempFile };
                    await rootCommand.InvokeAsync(args);
                    
                    var content = File.ReadAllText(tempFile);
                    content.Should().Contain($"# {rootCommand.Name}");
                    content.Should().Contain("*Test description*");
                    content.Should().Contain("--config");
                }
                finally
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }
        }
    }
}

