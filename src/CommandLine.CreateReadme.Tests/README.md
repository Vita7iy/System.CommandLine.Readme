# System.CommandLine.Readme Tests

Comprehensive unit test suite for the System.CommandLine.Readme library.

## Test Project Structure

```
CommandLine.CreateReadme.Tests/
├── System.CommandLine.Readme.Tests.csproj
├── MarkdownFormatHelperTests.cs       (82 tests)
├── CommandFormatHelperTests.cs        (30 tests)
└── CreateMarkDownTests.cs             (18 tests)
```

**Total: 130 unit tests**

## Running the Tests

### Using dotnet CLI

```bash
# From the src directory
cd /home/vitaly/src/System.CommandLine.Readme/src

# Build and run all tests
dotnet test CommandLine.CreateReadme.Tests/System.CommandLine.Readme.Tests.csproj

# Run with detailed output
dotnet test CommandLine.CreateReadme.Tests/System.CommandLine.Readme.Tests.csproj --logger:"console;verbosity=detailed"

# Run specific test class
dotnet test --filter "FullyQualifiedName~MarkdownFormatHelperTests"

# Run specific test method
dotnet test --filter "FullyQualifiedName~AsHeading"
```

### Using the test script

```bash
chmod +x run-tests.sh
./run-tests.sh
```

## Test Coverage

### MarkdownFormatHelperTests (82 tests)

Tests all markdown formatting extension methods:

| Method | Tests | Description |
|--------|-------|-------------|
| AsHeading | 4 | Heading generation (levels 1-6, null, invalid) |
| AddLineBreak | 3 | Double line break addition |
| AsParagraph | 2 | Paragraph formatting |
| AsBold | 3 | Bold text with `**` |
| AsItalic | 3 | Italic text with `*` |
| AsBoldAndItalic | 2 | Combined formatting `***` |
| AsCodeBlock | 4 | Code blocks with/without language |
| AsCodeInline | 3 | Inline code with backticks |
| AsBlockquote | 2 | Blockquote with `>` |
| AsNestedBlockquote | 2 | Nested blockquote `>>` |
| AsLink | 3 | Markdown links |
| AsOrderedList | 4 | Numbered lists |
| AsOrderedListItem | 4 | Individual numbered items |
| AsUnorderedList | 3 | Bulleted lists |
| AsUnorderedListItem | 2 | Individual bulleted items |
| AsTable | 3 | Markdown tables |
| AsTableHeader | 3 | Table headers with separators |
| AsNestedListItem | 2 | Nested list items |
| AddTab | 4 | Tab indentation |
| AddHorizontalRule | 1 | Horizontal rule `---` |

### CommandFormatHelperTests (30 tests)

Tests command-line formatting:

| Method | Tests | Description |
|--------|-------|-------------|
| FormatAliases | 4 | Alias formatting as inline code |
| FormatCommands | 4 | Command collection formatting |
| FormatRootCommand | 5 | Root command with all components |
| FormatCommand | 7 | Individual command formatting |
| FormatOptions | 7 | Option formatting with flags |
| FormatArgument | 4 | Argument formatting |
| FormatSymbols | 4 | Generic symbol formatting |

### CreateMarkDownTests (18 tests)

Tests readme generation and file operations:

| Test Class | Tests | Description |
|------------|-------|-------------|
| CreateReadme | 7 | Readme string generation |
| CreateReadmeFile | 4 | File creation and I/O |
| AddCommandLineReadmeToRoot | 7 | Extension method integration |

## Test Frameworks and Libraries

- **xUnit** 2.9.2 - Test framework
- **FluentAssertions** 6.12.1 - Fluent assertion library
- **Microsoft.NET.Test.Sdk** 17.11.1 - Test SDK
- **coverlet.collector** 6.0.2 - Code coverage

## Test Patterns

### Naming Convention
Tests use descriptive names that explain behavior:
```csharp
public void CreatesMarkdownTableFromRows()
public void ReturnsEmptyStringWhenTextIsNull()
public void HandlesComplexCommandStructure()
```

### Organization
Nested classes group related tests:
```csharp
public class MarkdownFormatHelperTests
{
    public class AsHeading { ... }
    public class AsBold { ... }
}
```

### Edge Cases Covered
- ✅ Null inputs
- ✅ Empty strings/collections
- ✅ Boundary values
- ✅ Invalid parameters
- ✅ Complex structures

### File I/O Tests
Proper cleanup with try-finally:
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

## Implementation Notes

### Internal Class Access

The test project accesses internal classes via `InternalsVisibleTo`:

**File**: `CommandLine.CreateReadme/AssemblyInfo.cs`
```csharp
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("System.CommandLine.Readme.Tests")]
```

### Async Test Methods

Following xUnit v3 recommendations, async tests use `async Task` instead of `async void`:

```csharp
[Fact]
public async Task ReadmeCommandCreatesFileWhenInvoked()
{
    await rootCommand.InvokeAsync(args);
    // assertions
}
```

### System.CommandLine API Usage

Tests use System.CommandLine 2.0 API correctly:

```csharp
// Adding items
rootCommand.Add(option);
command.Add(subcommand);
option.AddAlias("-o");

// Properties
option.IsRequired = true;

// Invocation
await rootCommand.InvokeAsync(args);
```

## Common Test Scenarios

### Testing Markdown Formatting
```csharp
[Fact]
public void AsBold_WrapTextWithDoubleAsterisks()
{
    var result = "Bold text".AsBold();
    result.Should().Be("**Bold text**");
}
```

### Testing Null Handling
```csharp
[Fact]
public void ReturnsEmptyStringWhenTextIsNull()
{
    string text = null;
    var result = text.AsBold();
    result.Should().BeEmpty();
}
```

### Testing File Generation
```csharp
[Fact]
public void CreatesFileAtSpecifiedPath()
{
    var tempFile = Path.GetTempFileName();
    try
    {
        var rootCommand = new RootCommand("Test");
        CreateMarkDown.CreateReadmeFile(rootCommand, tempFile);
        File.Exists(tempFile).Should().BeTrue();
    }
    finally
    {
        if (File.Exists(tempFile)) File.Delete(tempFile);
    }
}
```

### Testing Command Integration
```csharp
[Fact]
public async Task ReadmeCommandCreatesFileWhenInvoked()
{
    var tempFile = Path.GetTempFileName();
    try
    {
        var rootCommand = new RootCommand("My Application");
        rootCommand.AddCommandLineReadmeToRoot();
        
        await rootCommand.InvokeAsync(new[] { "readme", "--readme-file", tempFile });
        
        File.Exists(tempFile).Should().BeTrue();
    }
    finally
    {
        if (File.Exists(tempFile)) File.Delete(tempFile);
    }
}
```

## Troubleshooting

### Build Issues

If you encounter build errors:

1. Ensure .NET 8.0 SDK is installed:
   ```bash
   dotnet --version
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore CommandLine.CreateReadme.Tests/System.CommandLine.Readme.Tests.csproj
   ```

3. Clean and rebuild:
   ```bash
   dotnet clean
   dotnet build CommandLine.CreateReadme.Tests/System.CommandLine.Readme.Tests.csproj
   ```

### Test Failures

If tests fail:

1. Check that System.CommandLine.Readme project builds successfully
2. Verify temp file permissions
3. Check that no tests are being skipped
4. Run with verbose logging to see detailed output

## Contributing

When adding new tests:

1. Follow the existing naming conventions
2. Group related tests in nested classes
3. Test both happy path and edge cases
4. Include null/empty input tests
5. Clean up resources in finally blocks
6. Use FluentAssertions for readable assertions
7. Keep tests independent and isolated

## License

MIT

