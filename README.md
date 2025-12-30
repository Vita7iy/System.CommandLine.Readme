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

Generate the README file:

```bash
myapp readme --readme-file README.md
# or using aliases
myapp rm -md README.md
```

## 📖 Documentation

For detailed documentation, examples, and API reference, see the [package README](src/CommandLine.CreateReadme/Readme.md).

## 🏗️ Project Structure

```
System.CommandLine.Readme/
├── src/
│   ├── CommandLine.CreateReadme/          # Main library
│   │   ├── CreateMarkDown.cs              # Public API
│   │   ├── CommandFormatHelper.cs         # CLI formatting logic
│   │   ├── MarkdownFormatHelper.cs        # Markdown utilities
│   │   └── System.CommandLine.Readme.csproj
│   └── CommandLine.CreateReadme.Tests/    # Test suite
│       ├── CreateMarkDownTests.cs         # README generation tests
│       ├── CommandFormatHelperTests.cs    # CLI formatting tests
│       ├── MarkdownFormatHelperTests.cs   # Markdown utility tests
│       └── System.CommandLine.Readme.Tests.csproj
```

## 🧪 Testing

The library includes a comprehensive test suite with **130+ unit tests**:

- **82 tests** for markdown formatting helpers
- **30 tests** for command-line formatting logic
- **18 tests** for README generation functionality

Run all tests:

```bash
cd src
dotnet test
```

Run tests with detailed output:

```bash
dotnet test --logger:"console;verbosity=detailed"
```

See [TEST_SUMMARY.md](src/TEST_SUMMARY.md) for detailed test coverage information.

## 🛠️ Requirements

- **.NET Standard 2.0** or higher (compatible with .NET Framework 4.6.1+, .NET Core 2.0+, .NET 5+, 6+, 7+, 8+)
- **System.CommandLine** 2.0.0-beta4.22272.1 or compatible

## 💡 Example Generated Output

Given a CLI application with commands and options, the library generates markdown like:

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
```

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Add tests for new functionality (maintain high test coverage)
4. Ensure all tests pass (`dotnet test`)
5. Follow existing code style and conventions
6. Commit your changes (`git commit -m 'Add amazing feature'`)
7. Push to the branch (`git push origin feature/amazing-feature`)
8. Open a Pull Request

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

## 📄 License

This project is licensed under the MIT License.

## 🙏 Acknowledgments

Built as an extension for [System.CommandLine](https://github.com/dotnet/command-line-api) by Microsoft.

## 🔄 Version History

- **0.1.1-release**: Current stable version
  - Comprehensive markdown generation
  - Support for commands, options, arguments
  - Alias documentation
  - Type information display
  - Required flags
  - 130+ unit tests

---

**Made with ❤️ by Vita7y** | Copyright © 2024-2025
