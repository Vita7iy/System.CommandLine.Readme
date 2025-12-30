namespace System.CommandLine.Readme.Tests
{
    using FluentAssertions;
    using Xunit;

    public class MarkdownFormatHelperTests
    {
        public class AsHeading
        {
            [Theory]
            [InlineData("Title", 1, "# Title\n\n")]
            [InlineData("Title", 2, "## Title\n\n")]
            [InlineData("Title", 3, "### Title\n\n")]
            [InlineData("Title", 4, "#### Title\n\n")]
            [InlineData("Title", 5, "##### Title\n\n")]
            [InlineData("Title", 6, "###### Title\n\n")]
            public void ReturnsCorrectHeadingForValidLevels(string text, int level, string expected)
            {
                var result = text.AsHeading(level);
                result.Should().Be(expected);
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsHeading(1);
                result.Should().BeEmpty();
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(7)]
            public void ReturnsEmptyStringForInvalidLevels(int level)
            {
                var result = "Title".AsHeading(level);
                result.Should().BeEmpty();
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsEmpty()
            {
                var result = string.Empty.AsHeading(1);
                result.Should().Be("# \n\n");
            }
        }

        public class AddLineBreak
        {
            [Fact]
            public void AddsDoubleLineBreakToText()
            {
                var result = "Some text".AddLineBreak();
                result.Should().Be("Some text\n\n");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AddLineBreak();
                result.Should().BeEmpty();
            }

            [Fact]
            public void AddsLineBreakToEmptyString()
            {
                var result = string.Empty.AddLineBreak();
                result.Should().Be("\n\n");
            }
        }

        public class AsParagraph
        {
            [Fact]
            public void AddsDoubleLineBreakToText()
            {
                var result = "Paragraph text".AsParagraph();
                result.Should().Be("Paragraph text\n\n");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsParagraph();
                result.Should().BeEmpty();
            }
        }

        public class AsBold
        {
            [Fact]
            public void WrapTextWithDoubleAsterisks()
            {
                var result = "Bold text".AsBold();
                result.Should().Be("**Bold text**");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsBold();
                result.Should().BeEmpty();
            }

            [Fact]
            public void HandlesEmptyString()
            {
                var result = string.Empty.AsBold();
                result.Should().Be("****");
            }
        }

        public class AsItalic
        {
            [Fact]
            public void WrapTextWithSingleAsterisks()
            {
                var result = "Italic text".AsItalic();
                result.Should().Be("*Italic text*");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsItalic();
                result.Should().BeEmpty();
            }

            [Fact]
            public void HandlesEmptyString()
            {
                var result = string.Empty.AsItalic();
                result.Should().Be("**");
            }
        }

        public class AsBoldAndItalic
        {
            [Fact]
            public void WrapTextWithTripleAsterisks()
            {
                var result = "Bold and italic".AsBoldAndItalic();
                result.Should().Be("***Bold and italic***");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsBoldAndItalic();
                result.Should().BeEmpty();
            }
        }

        public class AsCodeBlock
        {
            [Fact]
            public void WrapTextWithTripleBackticks()
            {
                var result = "code".AsCodeBlock();
                result.Should().Be("```code```");
            }

            [Fact]
            public void WrapTextWithTripleBackticksAndType()
            {
                var result = "var x = 10;".AsCodeBlock("csharp");
                result.Should().Be("```csharp\nvar x = 10;\n```");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsCodeBlock();
                result.Should().BeEmpty();
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNullWithType()
            {
                string text = null;
                var result = text.AsCodeBlock("csharp");
                result.Should().BeEmpty();
            }
        }

        public class AsCodeInline
        {
            [Fact]
            public void WrapTextWithSingleBackticks()
            {
                var result = "code".AsCodeInline();
                result.Should().Be("`code`");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsCodeInline();
                result.Should().BeEmpty();
            }

            [Fact]
            public void HandlesSpecialCharacters()
            {
                var result = "--option".AsCodeInline();
                result.Should().Be("`--option`");
            }
        }

        public class AsBlockquote
        {
            [Fact]
            public void PrependGreaterThanSymbol()
            {
                var result = "Quote".AsBlockquote();
                result.Should().Be("> Quote");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsBlockquote();
                result.Should().BeEmpty();
            }
        }

        public class AsNestedBlockquote
        {
            [Fact]
            public void PrependDoubleGreaterThanSymbol()
            {
                var result = "Nested quote".AsNestedBlockquote();
                result.Should().Be(">> Nested quote");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsNestedBlockquote();
                result.Should().BeEmpty();
            }
        }

        public class AsLink
        {
            [Fact]
            public void CreatesMarkdownLink()
            {
                var result = "GitHub".AsLink("https://github.com");
                result.Should().Be("[GitHub](https://github.com)");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsLink("https://github.com");
                result.Should().BeEmpty();
            }

            [Fact]
            public void HandlesEmptyUrl()
            {
                var result = "Link".AsLink(string.Empty);
                result.Should().Be("[Link]()");
            }
        }

        public class AsOrderedList
        {
            [Fact]
            public void CreatesNumberedListFromItems()
            {
                var items = new[] { "First", "Second", "Third" };
                var result = items.AsOrderedList();
                result.Should().Be("1. First\n2. Second\n3. Third\n");
            }

            [Fact]
            public void ReturnsEmptyStringWhenItemsIsNull()
            {
                string[] items = null;
                var result = items.AsOrderedList();
                result.Should().BeEmpty();
            }

            [Fact]
            public void HandlesEmptyCollection()
            {
                var items = new string[] { };
                var result = items.AsOrderedList();
                result.Should().BeEmpty();
            }

            [Fact]
            public void HandlesSingleItem()
            {
                var items = new[] { "Only one" };
                var result = items.AsOrderedList();
                result.Should().Be("1. Only one\n");
            }
        }

        public class AsOrderedListItem
        {
            [Fact]
            public void CreatesNumberedListItem()
            {
                var result = "First item".AsOrderedListItem(1);
                result.Should().Be("1. First item\n");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsOrderedListItem(1);
                result.Should().BeEmpty();
            }

            [Fact]
            public void ReturnsEmptyStringForNegativeIndex()
            {
                var result = "Item".AsOrderedListItem(-1);
                result.Should().BeEmpty();
            }

            [Fact]
            public void HandlesZeroIndex()
            {
                var result = "Item".AsOrderedListItem(0);
                result.Should().Be("0. Item\n");
            }
        }

        public class AsUnorderedList
        {
            [Fact]
            public void CreatesBulletedListFromItems()
            {
                var items = new[] { "First", "Second", "Third" };
                var result = items.AsUnorderedList();
                result.Should().Be("- First\n- Second\n- Third\n");
            }

            [Fact]
            public void ReturnsEmptyStringWhenItemsIsNull()
            {
                string[] items = null;
                var result = items.AsUnorderedList();
                result.Should().BeEmpty();
            }

            [Fact]
            public void HandlesEmptyCollection()
            {
                var items = new string[] { };
                var result = items.AsUnorderedList();
                result.Should().BeEmpty();
            }
        }

        public class AsUnorderedListItem
        {
            [Fact]
            public void CreatesBulletedListItem()
            {
                var result = "Item".AsUnorderedListItem();
                result.Should().Be("- Item\n");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsUnorderedListItem();
                result.Should().BeEmpty();
            }
        }

        public class AsTable
        {
            [Fact]
            public void CreatesMarkdownTable()
            {
                var rows = new[]
                {
                    new[] { "Col1", "Col2" },
                    new[] { "Data1", "Data2" }
                };
                var result = rows.AsTable();
                result.Should().Be("|Col1|Col2|\n|Data1|Data2|\n");
            }

            [Fact]
            public void ReturnsEmptyStringWhenRowsIsNull()
            {
                string[][] rows = null;
                var result = rows.AsTable();
                result.Should().BeEmpty();
            }

            [Fact]
            public void HandlesSingleRow()
            {
                var rows = new[] { new[] { "Single" } };
                var result = rows.AsTable();
                result.Should().Be("|Single|\n");
            }
        }

        public class AsTableHeader
        {
            [Fact]
            public void CreatesMarkdownTableHeader()
            {
                var headers = new[] { "Name", "Value", "Description" };
                var result = headers.AsTableHeader();
                result.Should().Be("|Name|Value|Description|\n|---|---|---|\n");
            }

            [Fact]
            public void ReturnsEmptyStringWhenHeadersIsNull()
            {
                string[] headers = null;
                var result = headers.AsTableHeader();
                result.Should().BeEmpty();
            }

            [Fact]
            public void HandlesSingleHeader()
            {
                var headers = new[] { "Header" };
                var result = headers.AsTableHeader();
                result.Should().Be("|Header|\n|---|\n");
            }
        }

        public class AsNestedListItem
        {
            [Fact]
            public void CreatesNestedListItem()
            {
                var result = "Nested item".AsNestedListItem();
                result.Should().Be("* Nested item");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AsNestedListItem();
                result.Should().BeEmpty();
            }
        }

        public class AddTab
        {
            [Fact]
            public void AddsSingleTabByDefault()
            {
                var result = "Text".AddTab();
                result.Should().Be("\tText");
            }

            [Fact]
            public void AddsMultipleTabs()
            {
                var result = "Text".AddTab(3);
                result.Should().Be("\t\t\tText");
            }

            [Fact]
            public void ReturnsEmptyStringWhenTextIsNull()
            {
                string text = null;
                var result = text.AddTab();
                result.Should().BeEmpty();
            }

            [Fact]
            public void HandlesZeroTabs()
            {
                var result = "Text".AddTab(0);
                result.Should().Be("Text");
            }
        }

        public class AddHorizontalRule
        {
            [Fact]
            public void ReturnsMarkdownHorizontalRule()
            {
                var result = MarkdownFormatHelper.AddHorizontalRule();
                result.Should().Be("---\n");
            }
        }
    }
}

