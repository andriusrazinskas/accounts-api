using Accounts.WebApi.V1.AccountsImport;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
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
            var expectedResponse = new AccountsImportResponse();
            var fileContent = new StringContent("Thomas 3299992\nRichard 3293982\nMichael 4113902p", Encoding.UTF8, "text/plain");
            var formData = new MultipartFormDataContent { { fileContent, "file", "test.txt" } };

            var httpResponse = await _httpClient.PostAsync("/api/v1/accounts/import", formData);
            var actualResponse = await httpResponse.Content.ReadFromJsonAsync<AccountsImportResponse>();

            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.Equal(expectedResponse, actualResponse);
        }

        [Fact]
        public async Task ImportAccountsAsync_ReturnsBadRequestResponse_WhenUploadingInvalidFileData()
        {
            var expectedResponse = new AccountsImportErrorResponse(
            [
                "Account number - not valid for 1 line 'Thomas 32999921'",
                "Account name, account number - not valid for 3 line 'XAEA-12 8293982'",
                "Account number - not valid for 4 line 'Rose 329a982'",
                "Account number - not valid for 5 line 'Bob 329398.'",
                "Account name - not valid for 6 line 'michael 3113902'"
            ]);
            var fileContent = new StringContent("Thomas 32999921\n" + 
                                                "Richard 3293982\n" + 
                                                "XAEA-12 8293982\n" + 
                                                "Rose 329a982\n" + 
                                                "Bob 329398.\n" + 
                                                "michael 3113902\n" + 
                                                "Rob 3113902p", Encoding.UTF8, "text/plain");

            var formData = new MultipartFormDataContent { { fileContent, "file", "test.txt" } };

            var httpResponse = await _httpClient.PostAsync("/api/v1/accounts/import", formData);
            var actualResponse = await httpResponse.Content.ReadFromJsonAsync<AccountsImportErrorResponse>();

            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
            Assert.Equal(expectedResponse.FileValid, actualResponse?.FileValid);
            Assert.Equal(expectedResponse.InvalidLines, actualResponse?.InvalidLines);
        }
    }
}