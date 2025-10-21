namespace JazzFuzz.Game
{
    public class RuleEngine
    {
        private readonly IEnumerable<Rule> _rules;

        public RuleEngine(IEnumerable<Rule> rules)
        {
            _rules = rules;

            if (!_rules.Any())
                throw new InvalidOperationException("No rules to apply. Please add at least 1 rule");

            if (_rules.Any(x => string.IsNullOrWhiteSpace(x.Word)))
                throw new ArgumentException("Rules must have a non-empty word.");
        }

        /// <summary>
        /// Generates a sequence of strings based on the defined rules within the specified range.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public List<string> GenerateSequence(int start, int end)
        {
            if (start < 0 || end < 0)
                throw new ArgumentException("Start and end values must non-negative integers.");

            var sequence = new List<string>();

            if (start <= end)
            {
                for (int i = start; i <= end; i++)
                    sequence.Add(ApplyRules(i));
            }
            else
            {
                for (int i = start; i >= end; i--)
                    sequence.Add(ApplyRules(i));
            }

            return sequence;
        }

        /// <summary>
        /// Applies the defined rule to a given number and returns the resulting string.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private string ApplyRules(int number)
        {
            string result = string.Empty;

            foreach (var rule in _rules)
            {
                if (number % rule.Number == 0)
                    result += rule.Word;
            }

            return string.IsNullOrEmpty(result) ? number.ToString() : result;
        }
    }
}
