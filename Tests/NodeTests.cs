using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Tests;

public class NodeTests
{
    [Fact]
    public void TestAddWithSimpleDepth()
    {
        var node = new Node();
        node.PushAtLevel(0, new []{"name", "id"});

        node.Children.Count.Should().Be(2);
        node.Children.FirstOrDefault().Value.Should().Be("name");
        node.Children.LastOrDefault().Value.Should().Be("id");
    }

    [Fact]
    public void TestAddWithComplexDepth()
    {
        var node = new Node();
        node.PushAtLevel(0, new []{"name", "id", "customFields"});
        node.PushAtLevel(1, new []{"c1", "c2", "c3"});
        node.PushAtLevel(0, new [] {"last"});

        var nodeChildren = node.Children;
        var customFieldsNode = nodeChildren[2];
        customFieldsNode.Children.Count.Should().Be(3);
        customFieldsNode.Children.First().Value.Should().Be("c1");
        customFieldsNode.Children.Last().Value.Should().Be("c3");

        var lastNode = node.Children[3];
        lastNode.Value.Should().Be("last");
    }

    [Fact]
    public void TestToStringWithSimpleDepth()
    {
        var node = new Node();
        node.PushAtLevel(0, new []{"name", "id"});
        
        var actualOutput = node.ToString();
        var expectedOutput = "- name\n- id";

        actualOutput.Should().BeEquivalentTo(expectedOutput);
    }
    
    [Fact]
    public void TestToStringWithComplexDepth()
    {
        var node = new Node();
        node.PushAtLevel(0, new []{"name", "id", "customFields"});
        node.PushAtLevel(1, new []{"c1", "c2", "c3"});
        node.PushAtLevel(0, new [] {"last"});
        
        var actualOutput = node.ToString();
        
        var expectedOutput = "- name\n- id\n- customFields\n\t- c1\n\t- c2\n\t- c3\n- last";
    
        actualOutput.Should().BeEquivalentTo(expectedOutput);
    }
    
    
    [Fact]
    public void TestSortWithSimpleDepth()
    {
        var node = new Node();
        node.PushAtLevel(0, new []{"d", "a", "c", "b"});
        
        node.SortAlphabetically();

        node.Children.First().Value.Should().Be("a");
        node.Children.Last().Value.Should().Be("d");
    }
    
    [Fact]
    public void TestSortWithComplexDepth()
    {
        var node = new Node();
        node.PushAtLevel(0, new []{"name", "id", "customFields"});
        node.PushAtLevel(1, new []{"d", "a", "c", "b"});
        node.PushAtLevel(0, new [] {"agency"});
        
        node.SortAlphabetically();

        node.Children.First().Value.Should().Be("agency");
        node.Children.Last().Value.Should().Be("name");

        var customFieldsNode = node.Children.Find(x => x.Value == "customFields");
        customFieldsNode.Children.First().Value.Should().Be("a");
        customFieldsNode.Children.Last().Value.Should().Be("d");
    }
}