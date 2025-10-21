using JazzFuzz.Game;

namespace JazzFuzz.Tests
{
    public class RuleEngineTests
    {
        private readonly IEnumerable<Rule> _testRules = new List<Rule>
        {
            new Rule (3, "Fizz"),
            new Rule (5, "Buzz"),
            new Rule (7, "Jazz")
        };

        [Fact]
        public void GenerateSequence_AscendingSequence()
        {
            var ruleEngine = new RuleEngine(_testRules);

            var result = ruleEngine.GenerateSequence(1, 5);

            var expected = new List<string> { "1", "2", "Fizz", "4", "Buzz" };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateSequence_DescendingSequence()
        {
            var ruleEngine = new RuleEngine(_testRules);

            var result = ruleEngine.GenerateSequence(5, 1);

            var expected = new List<string> { "Buzz", "4", "Fizz", "2", "1" };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateSequence_Single_Number()
        {
            var ruleEngine = new RuleEngine(_testRules);

            var result = ruleEngine.GenerateSequence(15, 15);

            var expected = new List<string> { "FizzBuzz" };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateSequence_StartAndEndZero_SingleZero()
        {
            var ruleEngine = new RuleEngine(_testRules);

            var result = ruleEngine.GenerateSequence(0, 0);

            // 0 is divisible by all numbers, all rules apply.
            var expected = new List<string> { "FizzBuzzJazz" };
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateSequence_NegativeStart_ThrowsArgumentException()
        {
            var ruleEngine = new RuleEngine(_testRules);

            Assert.Throws<ArgumentException>(() => ruleEngine.GenerateSequence(-1, 5));
        }

        [Fact]
        public void GenerateSequence_NegativeEnd_ThrowsArgumentException()
        {
            var ruleEngine = new RuleEngine(_testRules);

            Assert.Throws<ArgumentException>(() => ruleEngine.GenerateSequence(1, -5));
        }

        [Fact]
        public void GenerateSequence_EmptyRules_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => new RuleEngine(new List<Rule>()));
        }

        [Fact]
        public void GenerateSequence_RulesIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RuleEngine(null));
        }

        [Fact]
        public void GenerateSequence_RulesWithEmptyWords_ThrowsArgumentException()
        {
            var rules = new List<Rule>
            {
                new Rule(1,""),
            };

            Assert.Throws<ArgumentException>(() => new RuleEngine(rules));

        }
    }
}
