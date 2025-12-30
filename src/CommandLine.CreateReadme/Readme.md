# System.CommandLine.Readme

[![NuGet](https://img.shields.io/badge/nuget-CommandLine.Readme-blue)](https://www.nuget.org/packages/CommandLine.Readme)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET Standard 2.0](https://img.shields.io/badge/.NET%20Standard-2.0-brightgreen)](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)

**System.CommandLine.Readme** is an extension library for [System.CommandLine](https://github.com/dotnet/command-line-api) that automatically generates professional README documentation in Markdown format from your command-line interface configuration.

## 🎯 Features

- **Automatic Documentation Generation**: Creates comprehensive markdown documentation from your CLI configuration
- **Complete Coverage**: Documents commands, subcommands, options, arguments, aliases, and descriptions
- **Hierarchical Structure**: Properly formatted nested command structures with appropriate indentation
- **Markdown Formatting**: Professional markdown output with headings, bold text, code blocks, and tables
- **Easy Integration**: Single line of code to add README generation capability
- **Type Information**: Includes value types and required flags for options and arguments
- **Extensible**: Built with internal formatting helpers for easy customization

## 📦 Installation

Install the package via NuGet:

```bash
dotnet add package CommandLine.Readme
```

Or using the Package Manager Console:

```powershell
Install-Package CommandLine.Readme
```

## 🚀 Quick Start

### Basic Usage

1. **Add the extension to your RootCommand:**

```csharp
using System.CommandLine;
using System.CommandLine.Readme;

var rootCommand = new RootCommand("My awesome CLI application");
var option = new Option<string>("--config", "Configuration file path");
rootCommand.AddOption(option);

// Add README generation capability
rootCommand.AddCommandLineReadmeToRoot();

return await rootCommand.InvokeAsync(args);
```

2. **Generate the README file:**

```bash
# Using the full command and option names
myapp readme --readme-file README.md

# Using aliases (shorter form)
myapp rm -md README.md
```

3. **Done!** Your `README.md` file is now generated with complete documentation of your CLI.

## 📖 Detailed Usage

### Advanced Example

```csharp
using System.CommandLine;
using System.CommandLine.Readme;

var rootCommand = new RootCommand("Advanced CLI application for data processing");

// Add global options
var verboseOption = new Option<bool>(
    "--verbose", 
    "Enable verbose logging");
verboseOption.AddAlias("-v");
rootCommand.AddOption(verboseOption);

// Add a subcommand
var processCommand = new Command("process", "Process data files");

var inputOption = new Option<FileInfo>(
    "--input", 
    "Input file to process") 
{ 
    IsRequired = true 
};
inputOption.AddAlias("-i");

var outputOption = new Option<FileInfo>(
    "--output", 
    "Output file path");
outputOption.AddAlias("-o");

processCommand.AddOption(inputOption);
processCommand.AddOption(outputOption);

// Add argument to subcommand
var formatArgument = new Argument<string>(
    "format", 
    "Output format (json, xml, csv)");
processCommand.AddArgument(formatArgument);

rootCommand.AddCommand(processCommand);

// Enable README generation
rootCommand.AddCommandLineReadmeToRoot();

return await rootCommand.InvokeAsync(args);
```

### Generated Output

The above example will generate a well-structured markdown file containing:

- Application name as main heading
- Horizontal separators for visual organization
- Root command description and options
- All subcommands with their options and arguments
- Aliases for commands and options
- Required flags for mandatory options
- Type information for all options and arguments
- Proper hierarchical structure with indentation

#### Example Generated Markdown:

```markdown
# myapp

---

## **myapp**: *Advanced CLI application for data processing*

## **myapp** options

### **--verbose**: *Enable verbose logging*
	Aliases: `-v`
	
	ValueType=*System.Boolean*

## **myapp** subcommands

* ### **process**: *Process data files*
	Aliases: `process`

* #### **process** options
	* ##### **--input**: *Input file to process*
		Aliases: `-i`
		
		***IsRequired;***
		
		ValueType=*System.IO.FileInfo*
	
	* ##### **--output**: *Output file path*
		Aliases: `-o`
		
		ValueType=*System.IO.FileInfo*
	
* #### **process** arguments:
	##### format : Output format (json, xml, csv)
```

## 🔧 API Reference

### Extension Methods

#### `AddCommandLineReadmeToRoot(this RootCommand rootCommand)`

Adds a `readme` command (alias: `rm`) to your root command with a `--readme-file` option (alias: `-md`).

**Example:**
```csharp
rootCommand.AddCommandLineReadmeToRoot();
```

### Static Methods

#### `CreateMarkDown.CreateReadme(RootCommand rootCommand)`

Generates markdown documentation as a string from the provided RootCommand.

**Parameters:**
- `rootCommand`: The RootCommand to document

**Returns:** `string` - The generated markdown content

**Example:**
```csharp
string markdown = CreateMarkDown.CreateReadme(rootCommand);
Console.WriteLine(markdown);
```

#### `CreateMarkDown.CreateReadmeFile(RootCommand rootCommand, string path)`

Generates and saves markdown documentation directly to a file.

**Parameters:**
- `rootCommand`: The RootCommand to document
- `path`: The file path where the README should be saved

**Example:**
```csharp
CreateMarkDown.CreateReadmeFile(rootCommand, "docs/CLI.md");
```

## 📝 Markdown Features

The library includes comprehensive markdown formatting capabilities through internal helpers:

- **Headings** (levels 1-6)
- **Text Formatting**: Bold, Italic, Bold+Italic
- **Code**: Inline code and code blocks (with language specification)
- **Lists**: Ordered, unordered, and nested lists
- **Blockquotes**: Single and nested
- **Tables**: Full table support with headers
- **Links**: Markdown-style hyperlinks
- **Horizontal Rules**
- **Tabs and Indentation**: For hierarchical structures

## 🏗️ Architecture

The library is organized into three main components:

### `CreateMarkDown` (Public API)
The main entry point providing:
- `CreateReadme(RootCommand)` - Generates markdown string
- `CreateReadmeFile(RootCommand, string)` - Generates and saves to file
- `AddCommandLineReadmeToRoot()` - Extension method for CLI integration

### `CommandFormatHelper` (Internal)
Handles System.CommandLine-specific formatting:
- `FormatRootCommand()` - Formats the root command with all its components
- `FormatCommand()` - Formats individual commands recursively
- `FormatOptions()` - Formats options with type info and required flags
- `FormatAliases()` - Formats command/option aliases
- `FormatSymbols()` - Generic symbol formatting

### `MarkdownFormatHelper` (Internal)
Provides 20+ markdown formatting extension methods:
- Text styling (bold, italic, code)
- Structure (headings, lists, tables)
- Special elements (blockquotes, links, horizontal rules)
- Layout (tabs, indentation, line breaks)

All internal classes are exposed to the test assembly via `InternalsVisibleTo` attribute for comprehensive testing.

## 🧪 Testing

The library includes a comprehensive test suite with **130+ unit tests** covering:

- **MarkdownFormatHelperTests** (82 tests) - All markdown formatting extension methods
- **CommandFormatHelperTests** (30 tests) - Command-line formatting logic
- **CreateMarkDownTests** (18 tests) - README generation functionality

### Test Coverage Includes:
- Happy path scenarios
- Edge cases (null values, empty strings, boundary conditions)
- Integration tests (file I/O, command invocation)
- API correctness validation

### Running Tests

Run all tests:

```bash
dotnet test
```

Run tests with detailed output:

```bash
dotnet test --logger:"console;verbosity=detailed"
```

Run specific test class:

```bash
dotnet test --filter "FullyQualifiedName~MarkdownFormatHelperTests"
```

Run with code coverage:

```bash
dotnet test /p:CollectCoverage=true
```

## 🛠️ Requirements

### Runtime Requirements
- **.NET Standard 2.0** or higher
  - Compatible with .NET Framework 4.6.1+
  - Compatible with .NET Core 2.0+
  - Compatible with .NET 5.0+
  - Compatible with .NET 6.0+
  - Compatible with .NET 7.0+
  - Compatible with .NET 8.0+

### Dependencies
- **System.CommandLine** (2.0.0-beta4.22272.1 or compatible)
  - The library this package extends

### Development Dependencies (Optional)
- **StyleCop.Analyzers** (1.2.0-beta.556) - For code quality analysis (Debug builds only)

## 💡 Best Practices

### Writing Good Descriptions
- **Be concise**: Keep command and option descriptions to one sentence when possible
- **Be specific**: Clearly state what the command/option does
- **Use imperative mood**: "Process files" instead of "Processes files"

### Command Structure
```csharp
// ✅ Good: Clear hierarchy
var rootCommand = new RootCommand("Application description");
var processCommand = new Command("process", "Process data files");
rootCommand.AddCommand(processCommand);

// ❌ Avoid: Too many levels of nesting (hard to read in documentation)
```

### Option Naming
```csharp
// ✅ Good: Consistent naming with clear aliases
var option = new Option<string>("--output-file", "Output file path");
option.AddAlias("-o");

// ✅ Good: Mark critical options as required
var inputOption = new Option<FileInfo>("--input", "Input file") 
{ 
    IsRequired = true 
};
```

### Documentation Workflow
1. **Develop your CLI**: Focus on functionality first
2. **Add descriptions**: Write clear descriptions for all commands/options
3. **Add README generation**: Call `AddCommandLineReadmeToRoot()`
4. **Generate documentation**: Run `myapp readme --readme-file README.md`
5. **Review and commit**: Include the generated README in your repository
6. **Keep in sync**: Regenerate the README whenever you add/modify CLI commands

### CI/CD Integration
```bash
# Add to your build/test pipeline to ensure README stays up-to-date
dotnet run -- readme --readme-file README.md
git diff --exit-code README.md || (echo "README is out of date!" && exit 1)
```

## 📋 Use Cases

- **Open Source Projects**: Automatically keep your CLI documentation in sync with code
- **Internal Tools**: Generate documentation for corporate CLI tools
- **API Clients**: Document CLI wrappers for web services
- **DevOps Tools**: Create self-documenting deployment and automation scripts
- **Educational Projects**: Help users understand available commands and options

## 🔍 Troubleshooting

### Common Issues

**Q: The `readme` command is not available in my CLI**  
A: Make sure you've called `rootCommand.AddCommandLineReadmeToRoot()` before invoking the command.

**Q: The generated README is missing some commands/options**  
A: Ensure all commands and options are added to the `RootCommand` before generating the README. The library can only document what's been configured.

**Q: I get a "file already exists" error**  
A: The library overwrites existing files by default. Ensure you have write permissions to the target directory.

**Q: How do I customize the output format?**  
A: Currently, the library uses a fixed format optimized for CLI documentation. For custom formatting, you can use `CreateMarkDown.CreateReadme()` to get the markdown string and modify it programmatically.

**Q: Can I use this with System.CommandLine 1.x?**  
A: This library is designed for System.CommandLine 2.0+. It may not work correctly with version 1.x due to API changes.

## 🤝 Contributing

Contributions are welcome! This project follows standard contribution guidelines:

1. **Fork the repository**
2. **Create a feature branch** (`git checkout -b feature/amazing-feature`)
3. **Add tests** for new functionality (maintain 100% coverage)
4. **Ensure all tests pass** (`dotnet test`)
5. **Follow coding standards** (StyleCop analyzers are enabled)
6. **Commit your changes** (`git commit -m 'Add amazing feature'`)
7. **Push to the branch** (`git push origin feature/amazing-feature`)
8. **Open a Pull Request**

### Development Setup
```bash
# Clone the repository
git clone <repository-url>
cd System.CommandLine.Readme/src

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run tests
dotnet test

# Run tests with coverage
dotnet test /p:CollectCoverage=true
```

### Code Quality
- All public APIs must have XML documentation comments
- Follow existing code style and patterns
- Internal helpers should be tested via `InternalsVisibleTo`
- Use FluentAssertions for test assertions

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🙏 Acknowledgments

Built as an extension for [System.CommandLine](https://github.com/dotnet/command-line-api) by Microsoft.

## 📞 Support

For issues, questions, or suggestions:
- Open an issue on the project repository
- Check existing documentation and tests for examples

## 🔄 Version History

- **0.1.1-release**: Current stable version
  - Comprehensive markdown generation
  - Support for commands, options, arguments
  - Alias documentation
  - Type information display
  - Required flags

---

**Made with ❤️ by Vita7y** | Copyright © 2025
