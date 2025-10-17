namespace JazzFuzz.Game
{
    public class RuleEngine
    {
        private readonly List<Rule> _rules;

        public RuleEngine(IEnumerable<Rule> rules)
        {
            _rules = rules.ToList();
        }

        public List<string> GenerateSequence(int start, int end)
        {
            if (!_rules.Any())
                throw new InvalidOperationException("No rules to apply. Please add at least 1 rule");

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
