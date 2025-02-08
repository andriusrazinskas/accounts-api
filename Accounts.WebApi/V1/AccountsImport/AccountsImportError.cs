namespace Accounts.WebApi.V1.AccountsImport
{
    public record AccountsImportError(IReadOnlyCollection<string> InvalidLines)
    {
        public bool FileValid => false;
    }
}
