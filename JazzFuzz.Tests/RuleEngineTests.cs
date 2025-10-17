using JazzFuzz.Game;

namespace JazzFuzz.Tests
{
    public class RuleEngineTests
    {
        [Theory]
        [InlineData(4, 4, "Fuzz")]
        [InlineData(9, 9, "Jazz")]
        [InlineData(72, 72, "JazzFuzz")]
        public void Test_Single_Number(int start, int end, string expectedOutput)
        {
            var customRules = new List<Rule>
            {
                new Rule(9, "Jazz"),
                new Rule(4, "Fuzz")
            };

            var ruleEngine = new RuleEngine(customRules);

            var sw = new StringWriter();
            Console.SetOut(sw);
            ruleEngine.Run(start, end);

            var result = sw.ToString().Trim();

            Assert.Equal(expectedOutput, result);
        }



        [Fact]
        public void Test_Sequence()
        {
            var customRules = new List<Rule>
            {
                new Rule(9, "Jazz"),
                new Rule(4, "Fuzz")
            };

            var ruleEngine = new RuleEngine(customRules);

            var sw = new StringWriter();
            Console.SetOut(sw);
            ruleEngine.Run(100, 70);

            var outputLines = sw.ToString().Trim()
                    .Split(Environment.NewLine);


            string[] expectedOutput = new string[]
            { "Fuzz", "Jazz", "98", "97", "Fuzz", "95", "94", "93", 
                "Fuzz", "91", "Jazz", "89", "Fuzz", "87", "86", "85", 
                "Fuzz", "83", "82", "Jazz", "Fuzz", "79", "78", "77", 
                "Fuzz", "75", "74", "73", "JazzFuzz", "71", "70"};

            Assert.Equal(expectedOutput, outputLines);
        }

        [Fact]
        public void Test_invalid_input()
        {
            var customRules = new List<Rule>
            {
                new Rule(9, "Jazz"),
                new Rule(4, "Fuzz")
            };

            var ruleEngine = new RuleEngine(customRules);

            Assert.Throws<ArgumentException>(() => ruleEngine.Run(-1, 5));
            Assert.Throws<ArgumentException>(() => ruleEngine.Run(1, -5));
        }
    }
}
