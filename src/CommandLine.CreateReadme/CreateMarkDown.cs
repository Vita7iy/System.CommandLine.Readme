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

        /// <summary>
        /// Adds the 'readme' command to the root command to create a ReadMe markdown file.
        /// <example>
        /// var rootCommand = new RootCommand("Example application.");
        /// rootCommand.AddCommandLineReadmeToRoot();
        ///
        /// // The following command creates a ReadMe.md file:
        /// // app.exe readme --readme-file ReadMe.md
        /// </example>
        /// </summary>
        /// <param name="rootCommand">RootCommand.</param>
        public static void AddCommandLineReadmeToRoot(this RootCommand rootCommand)
        {
            var readmeFileOption = new Option<FileInfo>(name: "--readme-file", "-md")
            {
                Required = true,
                Description = "The name of the ReadMe file.",
            };

            var readmeCommand = new Command("readme", "Create the 'Readme' Markdown file.")
            {
                readmeFileOption,
            };
            readmeCommand.Aliases.Add("rm");

            readmeCommand.SetAction(result => 
            {
                var readmeFile = result.GetValue(readmeFileOption);
                CreateMarkDown.CreateReadmeFile(rootCommand, readmeFile.FullName);
            });

            rootCommand.Add(readmeCommand);
        }
    }
}
