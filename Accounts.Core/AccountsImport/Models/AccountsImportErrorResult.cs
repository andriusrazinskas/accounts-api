namespace Accounts.Core.AccountsImport.Models
{
    public record AccountsImportErrorResult(string[] InvalidLines) : IAccountsImportResult;
}
