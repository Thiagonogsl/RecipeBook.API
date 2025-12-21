using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructure.DataAccess
{
    public class MyRecipeBookDbContext : DbContext
    {
        //Cria um construtor que recebe as opções como parametro e repassa para a classe base DbContext
        public MyRecipeBookDbContext(DbContextOptions<MyRecipeBookDbContext> options): base(options)
        {
        }

        //Avisa para o Entity Framework que existe uma tabela Users no banco de dados
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipeBookDbContext).Assembly);
        }
    }
}
