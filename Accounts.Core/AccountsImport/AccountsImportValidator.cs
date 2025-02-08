using System.Text.RegularExpressions;

namespace Accounts.Core.AccountsImport
{
    internal partial class AccountsImportValidator : IAccountsImportValidator
    {
        public string? ValidateAccountLine(string line, int lineNumber)
        {
            var lineParts = line.Split(' ');

            if (lineParts.Length != 2)
                return $"Line format is incorrect for {lineNumber} line '{line}'";

            var invalidProperties = new List<string>(lineParts.Length);

            if (!IsFirstNameValid(lineParts[0]))
                invalidProperties.Add("account name");

            if (!IsAccountNumberValid(lineParts[1]))
                invalidProperties.Add("account number");

            return invalidProperties.Count > 0 ? FormatErrorMessage(invalidProperties, line, lineNumber) : null;
        }

        private static string FormatErrorMessage(IReadOnlyCollection<string> invalidProperties, string line, int lineNumber)
        {
            var errorMessage = $"{string.Join(", ", invalidProperties)} - not valid for {lineNumber} line '{line}'";

            return string.Concat(errorMessage[0].ToString().ToUpper(), errorMessage.AsSpan(1));
        }

        private static bool IsFirstNameValid(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return false;

            if (!char.IsUpper(firstName, 0))
                return false;

            if (firstName.Any(c => !char.IsLetter(c)))
                return false;

            return true;
        }

        private static bool IsAccountNumberValid(string accountNumber) => AccountNumberRegex().IsMatch(accountNumber);

        [GeneratedRegex(@"^[34]\d{6}(p?)$")]
        private static partial Regex AccountNumberRegex();
    }
}
