using System.Net.Http.Json;
using System.Text.Json;

namespace JazzFuzz.Game
{
    public class DynamicRulesService
    {
        private readonly HttpClient _httpClient;

        // Real world scenario: Move the hardcoded url to a configuration file. Since we're in a console app, i'm just keeping it simple.
        private const string url = "https://epinova-fizzbuzz.azurewebsites.net/api/dynamic-rules";

        public DynamicRulesService(HttpClient? httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
        }

        /// <summary>
        /// Fetches custom rules from a remote API and generates a sequence based on those rules.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<string>> GetCustomSequence(int start, int end)
        {
            List<Rule> rules;

            try
            {
                rules = await _httpClient.GetFromJsonAsync<List<Rule>>(url) ?? new List<Rule>();
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException("Failed to retrieve rules from server (HTTP error).", e);
            }

            if (!rules.Any())
                throw new ArgumentException("No rules found in API", nameof(rules));

            PrintRules(rules);

            var gameEngine = new RuleEngine(rules);

            var generatedSequence = gameEngine.GenerateSequence(start, end);

            return generatedSequence;
        }

        /// <summary>
        /// Prints the active rules to the console.
        /// </summary>
        /// <param name="rules"></param>
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
