using JazzFuzz.Game;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;

namespace JazzFuzz.Tests
{
    public class DynamicRulesServiceTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _mockHttpClient;
        private readonly DynamicRulesService _service;

        public DynamicRulesServiceTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _service = new DynamicRulesService(_mockHttpClient);
        }

        [Fact]
        public async Task GetCustomSequence_WithValidRules_ReturnsGeneratedSequence()
        {
            var expectedRules = new List<Rule>
            {
                new Rule (3, "Fizz"),
                new Rule (5, "Buzz")
            };

            SetupHttpResponse(HttpStatusCode.OK, expectedRules);

            var customSequence = await _service.GetCustomSequence(1, 10);

            _mockHttpMessageHandler.Protected()
                .Verify("SendAsync", Times.Once(),
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>());

            var expectedSequence = new List<string> { "1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz" };

            Assert.Equal(customSequence, expectedSequence);

        }

        [Fact]
        public async Task GetCustomSequence_WithEmptyRules_ThrowsArgumentException()
        {
            SetupHttpResponse(HttpStatusCode.OK, new List<Rule>());
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetCustomSequence(1, 10));
        }

        [Fact]
        public async Task GetCustomSequence_ErrorFromApi_ThrowsHttpRequestException()
        {
            SetupHttpResponse(HttpStatusCode.InternalServerError, null);
            await Assert.ThrowsAsync<HttpRequestException>(() => _service.GetCustomSequence(1, 10));
        }

        private void SetupHttpResponse(HttpStatusCode statusCode, object? content)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = content == null ? null : JsonContent.Create(content)
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);
        }

    }
}
