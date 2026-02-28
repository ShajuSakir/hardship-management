using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace Hardship.Tests
{
    public class HardshipTests
     : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public HardshipTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Should_Create_Hardship_Record()
        {
            var request = new
            {
                customerName = "John Doe",
                dateOfBirth = DateTime.UtcNow.AddYears(-30),
                income = 5000,
                expenses = 3000,
                hardshipReason = "Temporary job loss"
            };

            var response = await _client.PostAsJsonAsync("/api/hardships", request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var getResponse = await _client.GetAsync("/api/hardships");

            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
