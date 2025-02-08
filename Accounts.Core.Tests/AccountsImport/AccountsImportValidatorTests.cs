using Accounts.Core.AccountsImport;

namespace Accounts.Core.Tests.AccountsImport
{
    public class AccountsImportValidatorTests
    {
        private readonly AccountsImportValidator _accountsImportValidator = new();

        [Fact]
        public void ValidateAccountLine_ReturnsErrorMessage_WhenLineFormatIsIncorrect()
        {
            const string line = "Thomas3299992";

            var errorMessage = _accountsImportValidator.ValidateAccountLine(line, 1);

            Assert.Equal("Line format is incorrect for 1 line 'Thomas3299992'", errorMessage);
        }

        [Fact]
        public void ValidateAccountLine_ReturnsErrorMessage_WhenFirstNameContainsNonAlphabeticCharacters()
        {
            const string line = "XAEA-12 3299992";

            var errorMessage = _accountsImportValidator.ValidateAccountLine(line, 1);

            Assert.Equal("Account name - not valid for 1 line 'XAEA-12 3299992'", errorMessage);
        }

        [Fact]
        public void ValidateAccountLine_ReturnsErrorMessage_WhenFirstNameDoesNotStartWithUppercaseCharacter()
        {
            const string line = "thomas 3299992";

            var errorMessage = _accountsImportValidator.ValidateAccountLine(line, 1);

            Assert.Equal("Account name - not valid for 1 line 'thomas 3299992'", errorMessage);
        }

        [Fact]
        public void ValidateAccountLine_ReturnsErrorMessage_WhenAccountNumberIsNot7Digits()
        {
            const string line = "Thomas 32999921";

            var errorMessage = _accountsImportValidator.ValidateAccountLine(line, 1);

            Assert.Equal("Account number - not valid for 1 line 'Thomas 32999921'", errorMessage);
        }

        [Fact]
        public void ValidateAccountLine_ReturnsErrorMessage_WhenAccountNumberContainsNonNumericCharacters()
        {
            const string line = "Thomas 329a982";

            var errorMessage = _accountsImportValidator.ValidateAccountLine(line, 1);

            Assert.Equal("Account number - not valid for 1 line 'Thomas 329a982'", errorMessage);
        }

        [Fact]
        public void ValidateAccountLine_ReturnsErrorMessage_WhenAccountNumberDoesNotStartWith3Or4()
        {
            const string line = "Thomas 5113902";

            var errorMessage = _accountsImportValidator.ValidateAccountLine(line, 1);

            Assert.Equal("Account number - not valid for 1 line 'Thomas 5113902'", errorMessage);
        }

        [Fact]
        public void ValidateAccountLine_ReturnsErrorMessage_WhenBothFirstNameAndAccountNumberAreInvalid()
        {
            const string line = "thomas 32999921";

            var errorMessage = _accountsImportValidator.ValidateAccountLine(line, 1);

            Assert.Equal("Account name, account number - not valid for 1 line 'thomas 32999921'", errorMessage);
        }

        [Theory]
        [InlineData("Thomas 3293982")]
        [InlineData("Thomas 3293982p")]
        public void ValidateAccountLine_ReturnsNull_WhenLineIsValid(string line)
        {
            var errorMessage = _accountsImportValidator.ValidateAccountLine(line, 1);

            Assert.Null(errorMessage);
        }
    }
}