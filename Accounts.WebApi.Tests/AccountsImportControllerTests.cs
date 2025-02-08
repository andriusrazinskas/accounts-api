using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;

namespace Accounts.WebApi.Tests
{
    public class AccountsImportControllerTests(WebApplicationFactory<Program> factory)
        : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient = factory.WithWebHostBuilder(_ => {}).CreateClient();

        [Fact]
        public async Task ImportAccountsAsync_ReturnsCreatedResponse_WhenUploadingValidFileData()
        {
            var fileContent = new StringContent("Thomas 3299992\nRichard 3293982\nMichael 4113902p", Encoding.UTF8, "text/plain");
            var formData = new MultipartFormDataContent { { fileContent, "file", "test.txt" } };

            var response = await _httpClient.PostAsync("/api/v1/accounts/import", formData);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task ImportAccountsAsync_ReturnsBadRequestResponse_WhenUploadingInvalidFileData()
        {
            var fileContent = new StringContent("Thomas 32999921\n" + 
                                                "Richard 3293982\n" + 
                                                "XAEA-12 8293982\n" + 
                                                "Rose 329a982\n" + 
                                                "Bob 329398.\n" + 
                                                "michael 3113902\n" + 
                                                "Rob 3113902p", Encoding.UTF8, "text/plain");

            var formData = new MultipartFormDataContent { { fileContent, "file", "test.txt" } };

            var response = await _httpClient.PostAsync("/api/v1/accounts/import", formData);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}