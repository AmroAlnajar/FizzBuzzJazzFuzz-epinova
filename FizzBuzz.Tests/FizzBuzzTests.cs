using FizzBuzz.Game;

namespace FizzBuzz.Tests
{
    public class FizzBuzzTests
    {
        [Fact]
        public void Run_PrintsFizzBuzzSequence()
        {
            var game = new FizzBuzzGame();
            var output = new StringWriter();
            Console.SetOut(output);
            game.Run(1, 5);

            var expected = string.Join(Environment.NewLine, new[] { "1", "2", "Fizz", "4", "Buzz", "" });

            Assert.Equal(expected, output.ToString());
        }
    }
}