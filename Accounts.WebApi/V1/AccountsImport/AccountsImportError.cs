namespace Accounts.WebApi.V1.AccountsImport
{
    public record AccountsImportError(string[] InvalidLines)
    {
        public bool FileValid => false;
    }
}
