using Accounts.Core.AccountsImport.Models;

namespace Accounts.Core.AccountsImport
{
    public interface IAccountsImporter
    {
        Task<IAccountsImportResult> ImportAccountsAsync(Stream stream, CancellationToken cancellationToken);
    }
}
