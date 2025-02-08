using Accounts.Core.AccountsImport;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            services
                .AddSingleton<IAccountsImporter, AccountsImporter>()
                .AddSingleton<IAccountsImportValidator, AccountsImportValidator>();

            return services;
        }
    }
}
