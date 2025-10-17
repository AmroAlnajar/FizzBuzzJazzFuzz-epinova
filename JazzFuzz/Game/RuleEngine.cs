namespace JazzFuzz.Game
{
    public class RuleEngine
    {
        private readonly List<Rule> _rules;

        public RuleEngine(IEnumerable<Rule> rules)
        {
            _rules = rules.ToList();
        }

        public void Run(int start, int end)
        {
            if (start < 0 || end < 0)
                throw new ArgumentException("Start and end values must non-negative integers.");

            if (start <= end)
            {
                for (int i = start; i <= end; i++)
                    Console.WriteLine(ApplyRules(i));
            }
            else
            {
                for (int i = start; i >= end; i--)
                    Console.WriteLine(ApplyRules(i));
            }
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
