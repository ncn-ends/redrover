using System.Diagnostics;
using App;
using FluentAssertions;
using Xunit;

namespace Tests;

public class AcceptanceTests
{
    private const string _input = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";

    [Fact]
    public void SolutionA()
    {
        var converter = new InputConverter(_input);
        var actualOutput = converter.GetOutput();
        
        const string expectedOutput = "- id\n- name\n- email\n- type\n\t- id\n\t- name\n\t- customFields\n\t\t- c1\n\t\t- c2\n\t\t- c3\n- externalId";

        actualOutput.Should().Be(expectedOutput);
    }
    
    
    [Fact]
    public void SolutionB()
    {
        var converter = new InputConverter(_input);
        var actualOutput = converter.GetOutputAlphabetical();
        
        const string expectedOutput = "- email\n- externalId\n- id\n- name\n- type\n\t- customFields\n\t\t- c1\n\t\t- c2\n\t\t- c3\n\t- id\n\t- name";

        actualOutput.Should().Be(expectedOutput);
    }
}