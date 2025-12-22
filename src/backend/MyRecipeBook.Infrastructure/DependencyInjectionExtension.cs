using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services)
        {
            // Add infrastructure related dependencies here
            AddDbContext_MySql(services);
            AddRepositories(services);
        }
        private static void AddDbContext_MySql(IServiceCollection services)
        {
            var connectionString = "Server=localhost;Database=meulivrodereceitas;Uid=root;Pwd=@Password123";

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 44));

            services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });

        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
