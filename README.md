# JazzFuzz
My implementation of the FizzBuzz game with advanced features like dynamic rules and API integration.

## Features

- Classic FizzBuzz implementation with extensible rules
- Dynamic rules fetched from external API
- Support for both ascending and descending sequences
- Comprehensive error handling and input validation
- Unit testing

## Project Structure

- `Game/` - Core game logic and rule processing
  - `DynamicRulesService.cs` - External API integration for rules
  - `RuleEngine.cs` - Main sequence generation logic
  - `Rule.cs` - Rule model and validation
- `Tests/` - Unit tests

## Usage

```csharp
// Basic usage with default rules
var fizzBuzzRules = new List<Rule>
{
    new Rule(3, "Fizz"),
    new Rule(5, "Buzz")
};

var fizzBuzzGame = new RuleEngine(fizzBuzzRules);
var fizzBuzzSequence = fizzBuzzGame.GenerateSequence(1, 100);
fizzBuzzSequence.ForEach(x => Console.WriteLine(x));

// Using dynamic rules from API
var dynamicRulesService = new DynamicRulesService();
var customSequence = await dynamicRulesService.GetCustomSequence(1, 100);
customSequence.ForEach(x => Console.WriteLine(x));
```

## Running the project

Run the project using:
```bash
dotnet run --project JazzFuzz
```


## Testing

Run tests using:
```bash
dotnet test
```