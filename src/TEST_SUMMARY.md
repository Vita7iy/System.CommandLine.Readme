# Unit Test Suite for System.CommandLine.Readme

## Overview
A comprehensive unit test project has been created for the System.CommandLine.Readme library. The test suite covers all functionality across three main areas:

## Test Files Created

### 1. MarkdownFormatHelperTests.cs (510 lines)
Tests for all markdown formatting extension methods:

- **AsHeading**: Tests heading creation at all levels (1-6), null handling, invalid levels
- **AddLineBreak**: Tests adding double line breaks, null handling
- **AsParagraph**: Tests paragraph formatting with line breaks
- **AsBold**: Tests bold text wrapping with `**`
- **AsItalic**: Tests italic text wrapping with `*`
- **AsBoldAndItalic**: Tests combined formatting with `***`
- **AsCodeBlock**: Tests code block creation with and without language type
- **AsCodeInline**: Tests inline code formatting with backticks
- **AsBlockquote**: Tests blockquote formatting with `>`
- **AsNestedBlockquote**: Tests nested blockquote with `>>`
- **AsLink**: Tests markdown link creation
- **AsOrderedList**: Tests numbered list creation from arrays
- **AsOrderedListItem**: Tests individual numbered list items
- **AsUnorderedList**: Tests bulleted list creation
- **AsUnorderedListItem**: Tests individual bulleted list items
- **AsTable**: Tests markdown table creation from 2D arrays
- **AsTableHeader**: Tests table header creation with separator row
- **AsNestedListItem**: Tests nested list item formatting
- **AddTab**: Tests tab indentation (single and multiple)
- **AddHorizontalRule**: Tests horizontal rule creation

Total: **82 test methods** covering happy paths, edge cases, and null handling

### 2. CommandFormatHelperTests.cs (365 lines)
Tests for command-line formatting helpers:

- **FormatAliases**: Tests alias formatting as inline code
- **FormatCommands**: Tests command collection formatting
- **FormatRootCommand**: Tests root command formatting with options, arguments, subcommands
- **FormatCommand**: Tests individual command formatting with all components
- **FormatOptions**: Tests option formatting including required flag and value types
- **FormatArgument**: Tests argument formatting
- **FormatSymbols**: Tests generic symbol formatting

Total: **30 test methods** covering various command structures and configurations

### 3. CreateMarkDownTests.cs (356 lines)
Tests for the main readme creation functionality:

- **CreateReadme**: Tests readme generation from RootCommand
  - Command name in heading
  - Description formatting
  - Options inclusion
  - Subcommands inclusion
  - Arguments inclusion
  - Complex command structures
  - Minimal command handling

- **CreateReadmeFile**: Tests file creation
  - File creation at specified path
  - Complete markdown content writing
  - Existing file overwriting
  - Directory creation

- **AddCommandLineReadmeToRoot**: Tests extension method integration
  - Readme command addition
  - Command description
  - Aliases (rm)
  - Options (--readme-file, -md)
  - Required flag
  - File creation via command invocation
  - Alias usage
  - Content validation

Total: **18 test methods** with async tests for command invocation

## Test Project Configuration

**File**: System.CommandLine.Readme.Tests.csproj
- Target Framework: net8.0
- Test SDK: Microsoft.NET.Test.Sdk 17.11.1
- Test Framework: xUnit 2.9.2
- Assertion Library: FluentAssertions 6.12.1
- Code Coverage: coverlet.collector 6.0.2

## Testing Approach

### Test Naming Convention
Tests use descriptive names that explain the behavior being tested:
- `CreatesReadmeWithRootCommandName`
- `ReturnsEmptyStringWhenTextIsNull`
- `HandlesComplexCommandStructure`

### Test Organization
Tests are organized using nested classes to group related test cases:
```csharp
public class MarkdownFormatHelperTests
{
    public class AsHeading { /* tests */ }
    public class AsBold { /* tests */ }
    // ...
}
```

### Test Coverage
- **Happy path**: Normal usage scenarios
- **Edge cases**: Empty strings, null values, boundary conditions
- **Integration**: File I/O, command invocation
- **API correctness**: Proper use of System.CommandLine 2.0 API

## Key Implementation Details

### System.CommandLine 2.0 API Usage
The tests use the correct API patterns:
- `rootCommand.Add(option)` instead of `rootCommand.Options.Add(option)`
- `command.AddAlias("alias")` for adding aliases
- `option.IsRequired` property for required options
- `await rootCommand.InvokeAsync(args)` for async command execution

### Internal Class Access
An `AssemblyInfo.cs` file was created with `[assembly: InternalsVisibleTo("System.CommandLine.Readme.Tests")]` to allow tests to access internal helper classes.

### File I/O Testing
Tests that create files use proper cleanup:
```csharp
var tempFile = Path.GetTempFileName();
try
{
    // test code
}
finally
{
    if (File.Exists(tempFile))
        File.Delete(tempFile);
}
```

## Total Test Count
**130 unit tests** covering all public and internal functionality

## Running the Tests
```bash
cd /home/vitaly/src/System.CommandLine.Readme/src
dotnet test CommandLine.CreateReadme.Tests/System.CommandLine.Readme.Tests.csproj
```

## Test Dependencies
All tests follow these principles:
- No external dependencies (except temp files)
- Isolated and independent
- Fast execution
- Clear assertions using FluentAssertions
- Self-descriptive test names

