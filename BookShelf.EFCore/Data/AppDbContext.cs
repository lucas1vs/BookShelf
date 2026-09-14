using BookShelf.Domain.Entits;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.EFCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
                
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=BookShelfDb;Trusted_Connection=True;TrustServerCertificate=True")
                .LogTo(Console.WriteLine);
        }

    }


}
