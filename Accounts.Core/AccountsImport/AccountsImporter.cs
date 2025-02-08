using Accounts.Core.AccountsImport.Models;

namespace Accounts.Core.AccountsImport
{
    internal class AccountsImporter(IAccountsImportValidator accountsImportValidator) : IAccountsImporter
    {
        public async Task<IAccountsImportResult> ImportAccountsAsync(Stream stream, CancellationToken cancellationToken)
        {
            using var streamReader = new StreamReader(stream);
            var validationErrors = new List<string>();
            var lineNumber = 0;

            while (await streamReader.ReadLineAsync(cancellationToken) is { } line)
            {
                lineNumber++;

                var validationError = accountsImportValidator.ValidateAccountLine(line, lineNumber);
                if (validationError is null)
                {
                    // parse, process account...
                }
                else
                {
                    validationErrors.Add(validationError);
                }
            }

            return validationErrors.Count == 0 ? new AccountsImportSuccessResult( ) : new AccountsImportErrorResult(validationErrors);
        }
    }
}
