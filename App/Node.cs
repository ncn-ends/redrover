using System.Diagnostics;

public interface INode
{
    void PushAtLevel(int level, IEnumerable<string> itemRange);
    string ToString();
    void SortAlphabetically();
    string Value { get; set; }
    List<Node>? Children { get; set; }
}

/// <summary>
///     Recursive data structure referencing itself to represent a tree structure.
/// </summary>
public class Node : INode
{
    public string Value { get; set; } = "";
    public List<Node>? Children { get; set; }

    /// <summary>
    ///     Pushes an IEnumerable structure at the end of the Children property at the specified level of the tree structure.
    /// </summary>
    /// <param name="level">The level which should be added to. 0 represents the top level. Min: 0</param>
    /// <param name="itemRange">The IEnumerable to be added to the Children's property at the specified level.</param>
    public void PushAtLevel(int level, IEnumerable<string> itemRange)
    {
        if (level < 0) throw new ArgumentException("Cannot accept negative numbers for level.");
        
        if (level == 0)
        {
            Children ??= new List<Node>();
            
            foreach (var item in itemRange)
            {
                Children.Add(new Node
                {
                    Value = item
                });
            }

            return;
        }

        if (Children is null) throw new Exception("Attempting to add to children that don't exist");
        var lastChild = Children.LastOrDefault();
        if (lastChild is null) throw new Exception("Attempting to add to last child that doesn't exist");
        lastChild.PushAtLevel(level - 1, itemRange);
    }

    /// <summary>
    ///     Converts the tree structure into a string.
    ///     Nesting is represented by indentation. 
    ///     Each item in a list is represented by a new line.
    /// </summary>
    /// <returns>String representing the tree structure</returns>
    public override string ToString()
    {
        return Children is null ? Value : FormatToString(Children);
    }

    private string FormatToString(List<Node> children, string rollingOutput = "")
    {
        var output = rollingOutput;
        
        foreach (var child in children)
        {
            if (output != "") output += Environment.NewLine;
            output += $"- {child.Value}";

            if (child.Children is null) continue;
            
            output += ("\n" + FormatToString(child.Children, rollingOutput)).Replace("\n", "\n\t");
        }

        return output;
    }

    /// <summary>
    ///     Sorts all Children properties of the tree structure at every level alphabetically
    /// </summary>
    public void SortAlphabetically()
    {
        if (Children is null) return;

        var childrenEnumerable = (IEnumerable<Node>) Children;
        
        Children = childrenEnumerable.OrderBy(p => p.Value).ToList();
        
        foreach (var child in Children) child.SortAlphabetically();
    }
}