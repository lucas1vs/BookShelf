using BookShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BookShelf.EFCore.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {

    }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookCopy> BookCopies { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=BookShelfDb;Trusted_Connection=True;TrustServerCertificate=True")
            .LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(b => b.Country).HasMaxLength(56).IsRequired();
            entity.Property(b => b.State).HasMaxLength(50).IsRequired();
            entity.Property(b => b.City).HasMaxLength(100).IsRequired();
            entity.Property(b => b.Neighborhood).HasMaxLength(100).IsRequired();
            entity.Property(b => b.CEP).HasMaxLength(8).IsRequired();
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(b => b.Name).HasMaxLength(150).IsRequired();
            entity.Property(n => n.Biography).HasMaxLength(2000);
            entity.Property(n => n.Gender).HasConversion<string>().HasMaxLength(150).IsRequired();
        });
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(b => b.AuthorId).IsRequired();
            entity.Property(b => b.Title).HasMaxLength(250).IsRequired();
            entity.Property(b => b.Genre).HasMaxLength(25).IsRequired();
            entity.Property(b => b.Pages).IsRequired();
            entity.Property(b => b.YearOfPublication).IsRequired();
            entity.Property(b => b.Synopsis).HasMaxLength(4000);

        });

        modelBuilder.Entity<BookCopy>(entity =>
        {
            entity.Property(c => c.BookId).IsRequired();
            entity.Property(c => c.InventoryCode).HasMaxLength(30).IsRequired();
            entity.Property(c => c.AcquisitionDate).IsRequired();
            entity.Property(c => c.PhysicalCondition).IsRequired();
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(c => c.Name).HasMaxLength(150).IsRequired();
            entity.Property(c => c.CPF).HasMaxLength(11).IsRequired();
            entity.Property(c => c.DateOfBirth).IsRequired();
            entity.Property(c => c.Email).HasMaxLength(254).IsRequired();
            entity.Property(c => c.DateOfRegister).IsRequired();
            entity.Property(c => c.Status).HasMaxLength(150).IsRequired();
            entity.Property(c => c.Plan).IsRequired();
            entity.Property(c => c.AddressId).IsRequired();
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.Property(l => l.ClientId).IsRequired();
            entity.Property(l => l.BookCopyId).IsRequired();
            entity.Property(l => l.LoanDate).IsRequired();
            entity.Property(l => l.DueDate).IsRequired();
        });
    }
}
