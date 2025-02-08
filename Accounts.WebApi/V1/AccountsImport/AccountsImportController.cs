using Microsoft.AspNetCore.Mvc;

namespace Accounts.WebApi.V1.AccountsImport
{
    [ApiController]
    [Route("api/v1/accounts/import")]
    public class AccountsImportController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ImportAccountsAsync(IFormFile file, CancellationToken cancellationToken)
        {
            return Created();
        }
    }
}