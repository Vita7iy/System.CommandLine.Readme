// Ignore Spelling: Readme

namespace System.CommandLine.Readme
{
    using System.CommandLine;
    using System.IO;
    using System.Text;

    public static class CreateMarkDown
    {
        public static string CreateReadme(RootCommand rootCommand)
        {
            var readme = new StringBuilder();

            readme.Append(rootCommand.Name.AsHeading(1));
            readme.Append(CommandFormatHelper.FormatRootCommand(rootCommand));

            return readme.ToString();
        }

        public static void CreateReadmeFile(RootCommand rootCommand, string path)
        {
            var readme = CreateReadme(rootCommand);
            System.IO.File.WriteAllText(path, readme);
        }

        public static void AddCommandLineReadmeToRoot(this RootCommand rootCommand)
        {
            var readmeFileOption = new Option<FileInfo>(
                name: "--readme-file",
                description: "The ReadMe file.")
            { IsRequired = true };
            readmeFileOption.AddAlias("-md");

            var readmeCommand = new Command("readme", "Create Readme markdown file")
            {
                readmeFileOption,
            };
            readmeCommand.AddAlias("rm");

            readmeCommand.SetHandler(
                (readmeFile) =>
            {
                CreateMarkDown.CreateReadmeFile(rootCommand, readmeFile.FullName);
            },
                readmeFileOption);

            rootCommand.AddCommand(readmeCommand);
        }
    }
}
