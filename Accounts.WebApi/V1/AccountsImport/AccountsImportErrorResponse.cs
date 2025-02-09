namespace Accounts.WebApi.V1.AccountsImport
{
    public record AccountsImportErrorResponse(IReadOnlyCollection<string> InvalidLines)
    {
        public bool FileValid => false;
    }
}
