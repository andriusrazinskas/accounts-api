using Accounts.Core.AccountsImport;
using Accounts.Core.AccountsImport.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Accounts.WebApi.V1.AccountsImport
{
    [ApiController]
    [Route("api/v1/accounts/import")]
    [ProducesResponseType<AccountsImportSuccess>((int)HttpStatusCode.Created)]
    [ProducesResponseType<AccountsImportError>((int)HttpStatusCode.BadRequest)]
    public class AccountsImportController(IAccountsImporter accountsImporter) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ImportAccountsAsync(IFormFile file, CancellationToken cancellationToken)
        {
            await using var stream = file.OpenReadStream();

            var result = await accountsImporter.ImportAccountsAsync(stream, cancellationToken);

            return result switch
            {
                AccountsImportSuccessResult => Created(null as string, new AccountsImportSuccess()),
                AccountsImportErrorResult errorResult => BadRequest(new AccountsImportError(errorResult.InvalidLines)),
                _ => throw new InvalidOperationException("Unexpected import result.")
            };
        }
    }
}