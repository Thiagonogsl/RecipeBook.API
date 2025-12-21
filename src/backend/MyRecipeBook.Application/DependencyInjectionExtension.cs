using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            // Add application related dependencies here
            AddRepositories(services);
            AddDbContext_MySql(services);
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
