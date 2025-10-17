using System.Net.Http.Json;

namespace JazzFuzz.Game
{
    public class DynamicRulesService
    {
        private readonly HttpClient _httpClient;
        private const string url = "https://epinova-fizzbuzz.azurewebsites.net/api/dynamic-rules";

        public DynamicRulesService(HttpClient? httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
        }

        public async Task PlayAsync(int start, int end)
        {
            var rules = await _httpClient.GetFromJsonAsync<List<Rule>>(url) ?? new List<Rule>();

            if (!rules.Any())
                throw new ArgumentException("No rules found in API", nameof(rules));

            PrintRules(rules);

            var game = new RuleEngine(rules);
            game.Run(start, end);
        }

        private void PrintRules(IEnumerable<Rule> rules)
        {
            Console.WriteLine("Active rules:");
            foreach (var rule in rules)
            {
                Console.WriteLine($"{rule.Number} → {rule.Word}");
            }
            Console.WriteLine();
        }
    }
}
