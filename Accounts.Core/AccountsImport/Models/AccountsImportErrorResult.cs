namespace Accounts.Core.AccountsImport.Models
{
    public record AccountsImportErrorResult(IReadOnlyCollection<string> InvalidLines) : IAccountsImportResult;
}
