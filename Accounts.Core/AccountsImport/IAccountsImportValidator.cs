namespace Accounts.Core.AccountsImport
{
    internal interface IAccountsImportValidator
    {
        /// <summary>
        /// Validates single line of account data.
        /// </summary>
        /// <returns>Error message or <c>null</c> if there are no errors.</returns>
        string? ValidateAccountLine(string line, int lineNumber);
    }
}
