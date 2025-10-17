using JazzFuzz.Game;

Console.WriteLine("=== FizzBuzz (1 to 100) ===");

var fizzBuzzRules = new List<Rule>
{
    new Rule(3, "Fizz"),
    new Rule(5, "Buzz")
};

var fizzBuzzGame = new RuleEngine(fizzBuzzRules);
var fizzBuzzSequence = fizzBuzzGame.GenerateSequence(1, 100);
fizzBuzzSequence.ForEach(x => Console.WriteLine(x));

Console.WriteLine("\n=== JazzFuzz (100 to 1) ===");

var customRules = new List<Rule>
{
    new Rule(9, "Jazz"),
    new Rule(4, "Fuzz")
};
var ruleEngine = new RuleEngine(customRules);

var jazzFuzzSequence = ruleEngine.GenerateSequence(100, 1);
jazzFuzzSequence.ForEach(x => Console.WriteLine(x));