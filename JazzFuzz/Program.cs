using JazzFuzz.Game;

Console.WriteLine("=== FizzBuzz (1 to 100) ===");

var fizzBuzzRules = new List<Rule>
{
    new Rule(3, "Fizz"),
    new Rule(5, "Buzz")
};

var fizzBuzzGame = new JazzFuzzGame(fizzBuzzRules);
fizzBuzzGame.Run(1, 100);

Console.WriteLine("\n=== JazzFuzz (100 to 1) ===");

var jazzFuzzRules = new List<Rule>
{
    new Rule(9, "Jazz"),
    new Rule(4, "Fuzz")
};

var JazzFuzzGame = new JazzFuzzGame(jazzFuzzRules);
JazzFuzzGame.Run(100, 1);