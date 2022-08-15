using System.Diagnostics;
using System.Linq;
using App;
using FluentAssertions;
using Xunit;

namespace Tests;

public class InputConverterTests
{
    [Theory]
    [InlineData("(a, b, c)", 0, 8, '(', ')')]
    [InlineData("(a, b, c(d, e, f), g)", 0, 8, '(', '(')]
    [InlineData("(a, b, c(d, e, f, (h)), g)", 0, 8, '(', '(')]
    public void TestGetSymbolData(
        string input,
        int firstSymbolIndexExpected,
        int secondSymbolIndexExpected,
        char firstSymbolExpected,
        char secondSymbolExpected)
    {
        var inputConverter = new InputConverter(input);
    
        var symbolPositions = inputConverter.GetSymbolPositions();
        
        symbolPositions.FirstSymbolIndex.Should().Be(firstSymbolIndexExpected);
        symbolPositions.SecondSymbolIndex.Should().Be(secondSymbolIndexExpected);
        symbolPositions.FirstSymbol.Should().Be(firstSymbolExpected);
        symbolPositions.SecondSymbol.Should().Be(secondSymbolExpected);
    }

    [Fact]
    public void TestInputFormatterWithSimpleDepth()
    {
        const string input = "(a, b, c)";
        var inputConverter = new InputConverter(input);
        
        var structure = inputConverter.GetStructure();
        
        structure.Children.First().Value.Should().Be("a");
        structure.Children.Last().Value.Should().Be("c");
    }
    
    
    [Fact]
    public void TestInputFormatterWithComplexDepth()
    {
        const string input = "(a, b, c(d, e, f), g)";
        var inputConverter = new InputConverter(input);
        
        var structure = inputConverter.GetStructure();
        
        structure.Children.First().Value.Should().Be("a");
        structure.Children.Last().Value.Should().Be("g");

        var cNode = structure.Children.Find(x => x.Value == "c");
        cNode.Children.First().Value.Should().Be("d");
        cNode.Children.Last().Value.Should().Be("f");
    }
    
    
    
    [Fact]
    public void TestInputFormatterAndOutput()
    {
        const string input = "(a, b, c(d, e, f), g)";
        var inputConverter = new InputConverter(input);
        
        var structure = inputConverter.GetStructure();
        var actualOutput = structure.ToString();
        
        const string expectedOutput = "- a\n- b\n- c\n\t- d\n\t- e\n\t- f\n- g";
        actualOutput.Should().Be(expectedOutput);
    }
}