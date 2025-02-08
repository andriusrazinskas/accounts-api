using Accounts.Core.AccountsImport;
using Accounts.Core.AccountsImport.Models;
using NSubstitute;
using System.Text;

namespace Accounts.Core.Tests.AccountsImport
{
    public class AccountsImporterTests
    {
        private readonly IAccountsImportValidator _accountsImportValidator;
        private readonly AccountsImporter _accountsImporter;

        public AccountsImporterTests()
        {
            _accountsImportValidator = Substitute.For<IAccountsImportValidator>();
            _accountsImporter = new AccountsImporter(_accountsImportValidator);
        }

        [Fact]
        public async Task ImportAccountsAsync_ReturnsSuccessResult_WhenFileIsEmpty()
        {
            using var stream = new MemoryStream();

            var result = await _accountsImporter.ImportAccountsAsync(stream, default);

            Assert.IsType<AccountsImportSuccessResult>(result);
        }

        [Fact]
        public async Task ImportAccountsAsync_ValidatesEachLine()
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Thomas 3299992\nRichard 3293982\n"));

            await _accountsImporter.ImportAccountsAsync(stream, default);

            _accountsImportValidator.Received().ValidateAccountLine("Thomas 3299992", 1);
            _accountsImportValidator.Received().ValidateAccountLine("Richard 3293982", 2);
        }

        [Fact]
        public async Task ImportAccountsAsync_ReturnsSuccessResult_WhenAllFileLinesAreValid()
        {
            _accountsImportValidator
                .ValidateAccountLine(Arg.Any<string>(), Arg.Any<int>())
                .Returns(null as string);

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Thomas 3299992\nRichard 3293982\n"));

            var result = await _accountsImporter.ImportAccountsAsync(stream, default);

            Assert.IsType<AccountsImportSuccessResult>(result);
        }

        [Fact]
        public async Task ImportAccountsAsync_ReturnsErrorResult_WhenFileHasInvalidLine()
        {
            _accountsImportValidator
                .ValidateAccountLine(Arg.Any<string>(), Arg.Any<int>())
                .Returns("Account number - not valid for line 1 'Thomas 32999921'", null as string);

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Thomas 329%992\nRichard 3293982\n"));

            var result = await _accountsImporter.ImportAccountsAsync(stream, default);

            var errorResult = Assert.IsType<AccountsImportErrorResult>(result);
            Assert.Equal(["Account number - not valid for line 1 'Thomas 32999921'"], errorResult.InvalidLines);
        }
    }
}