using App;

const string input = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
var converter = new InputConverter(input);
var solutionA = converter.GetOutput();
var solutionB = converter.GetOutputAlphabetical();

Console.WriteLine(solutionA);
Console.WriteLine("\n\n");
Console.WriteLine(solutionB);