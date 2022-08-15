using System.Diagnostics;

namespace App;

public class SymbolPositions
{
    public char FirstSymbol { get; set; }
    public char SecondSymbol { get; set; }
    public int FirstSymbolIndex { get; set; } = -1;
    public int SecondSymbolIndex { get; set; } = -1;
}

/// <summary>
///     Converts the input string into a workable tree structure.
/// </summary>
/// <example>
/// This shows how to convert an input string into a workable tree structure.
/// <code>
/// const string input = "(a, b, c)";
/// var inputConverter = new InputConverter(input);
/// </code>
/// </example>
public class InputConverter
{
    private readonly string _input;
    private readonly INode _node;

    public InputConverter(string input, INode? node = null)
    {
        _input = input;
        _node = node ?? new Node();
        Convert();
    }

    public INode GetStructure()
    {
        return _node;
    }

    
    private void Convert()
    {
        var shrinkingInput = _input;
        int level = 0;

        /* the only way for level to go below 0 is if the final closing round bracket has been reached */
        while (level > -1)
        {
            var symbolPositions = GetSymbolPositions(shrinkingInput);

            var secondSymbol = symbolPositions.SecondSymbol;
            var firstSymbolIndex = symbolPositions.FirstSymbolIndex;
            var secondSymbolIndex = symbolPositions.SecondSymbolIndex;

            /* add values between first and second symbol to tree */
            var str = shrinkingInput.Substring(firstSymbolIndex + 1, secondSymbolIndex - firstSymbolIndex - 1);
            var values = ExtractValuesFromString(str);
            _node.PushAtLevel(level, values);

            /* level navigation only requires the second symbol */
            if (secondSymbol == ')') level--;
            else if (secondSymbol == '(') level++;

            shrinkingInput = shrinkingInput[secondSymbolIndex..];
        }
    }

    /// <summary>
    ///     Converts a string into an IEnumerable and sanitizes it so
    ///     that can be used to add to the tree structure as node values.
    /// </summary>
    /// <param name="str">The input string to work off of</param>
    /// <returns>A collection to be added to the tree structure as values</returns>
    private IEnumerable<string> ExtractValuesFromString(string str)
    {
        return str
            .Replace("(", "")
            .Replace(")", "")
            .Split(", ")
            .Where(x => x != "");
    }

    /// <summary>
    ///     Constructs all necessary data related to symbols in the working string, specifically ( and )
    /// </summary>
    /// <param name="input">Input string to work off of</param>
    /// <returns>SymbolPositions type, which includes properties for the first and second symbols</returns>
    public SymbolPositions GetSymbolPositions(string? input = null)
    {
        input ??= _input;

        int firstOpen, firstClose, secondOpen = -1, secondClose = -1;
        firstOpen = input.IndexOf("(");
        firstClose = input.IndexOf(")");
        if (firstOpen != -1)
        {
            var asd = input[(firstOpen + 1)..].IndexOf("(");
            if (asd != -1) secondOpen = asd + firstOpen + 1;
        }
        if (firstClose != -1)
        {
            var asd = input[(firstClose + 1)..].IndexOf(")");
            if (asd != -1) secondClose = asd + firstClose + 1;
        }
        
        IEnumerable<int> symbols = new[]
        {
            firstOpen, 
            firstClose, 
            secondOpen, 
            secondClose
        };

        var orderedSymbols = symbols
            .Where(x => x > -1)
            .OrderBy(p => p)
            .ToArray();

        return new SymbolPositions
        {
            FirstSymbol = input[orderedSymbols[0]],
            SecondSymbol = input[orderedSymbols[1]],
            FirstSymbolIndex = orderedSymbols[0],
            SecondSymbolIndex = orderedSymbols[1]
        };
    }

    public string GetOutput()
    {
        return _node.ToString();
    }
    
    public string GetOutputAlphabetical()
    {
        _node.SortAlphabetically();
        return _node.ToString();
    }
}

