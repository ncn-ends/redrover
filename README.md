## Usage

- You can run the program with `dotnet run` in the App directory
    - This will output the solution answers to the console
- You can also run the tests in the Tests directory
    - The solutions are part of the `AcceptanceTests` class

## Reasoning
- Doesn't specify what data structure should be used for the output
  - Normally would ask to confirm, but ambiguity might be part of the problem
  - I think it should be a string output, since the order of keys usually doesn't matter if it's a dictionary / object
- 2 outputs with the same values but different format are required 
  - At first I figured this probably indicates more outputs may be needed in the future
  - But then it doesn't make sense why you would need to define output explicitly, since you can then just use the output format to get the output without needing the input string at all
  - This indicates there's a different pattern
- The first solution is easy because it resembles the original format if round brackets represent
  nesting
- Upon closer inspection the second solution is simply sorted alphabetically
  - This means different formats probably don't need to be explicitly defined