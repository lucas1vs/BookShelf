using BookShelf.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            .UseSqlServer(AppConfig.GetConnectionString())
            .LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(a => a.Country).HasMaxLength(56).IsRequired();
            entity.Property(a => a.State).HasMaxLength(50).IsRequired();
            entity.Property(a => a.City).HasMaxLength(100).IsRequired();
            entity.Property(a => a.Neighborhood).HasMaxLength(100).IsRequired();
            entity.Property(a => a.CEP).HasMaxLength(8).IsRequired();
            entity.HasCheckConstraint("CK_Clients_CEP_ValidCep", "[CEP] NOT LIKE '%[^0-9]%' AND LEN([CEP]) = 8");
            
            



        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(a => a.Name).HasMaxLength(150).IsRequired();
            entity.Property(a => a.Biography).HasMaxLength(2000);
            entity.Property(a => a.Gender).HasConversion<string>().HasMaxLength(8);
           

        });
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasCheckConstraint("CK_Book_PagesPositivas", "Pages > 0");
            entity.Property(b => b.Title).HasMaxLength(250).IsRequired();
            entity.Property(b => b.Genre).HasConversion<string>().HasMaxLength(25);
            entity.Property(b => b.Synopsis).HasMaxLength(4000);
            entity.HasOne(b => b.Author)
            .WithMany(b => b.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BookCopy>(entity =>
        {
            entity.Property(bc => bc.InventoryCode).HasMaxLength(30).IsRequired();
            entity.HasIndex(bc => bc.InventoryCode).IsUnique();
            entity.Property(bc => bc.PhysicalCondition).HasConversion<string>().HasMaxLength(10);
            entity.HasOne(bc => bc.Book)
            .WithMany(bc => bc.BookCopies)
            .HasForeignKey(bc => bc.BookId)
            .OnDelete(DeleteBehavior.Restrict);


        }); 

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(c => c.Name).HasMaxLength(150).IsRequired();
            entity.Property(c => c.Gender).HasConversion<string>().HasMaxLength(8);
            entity.Property(c => c.CPF).HasMaxLength(11).IsRequired();
            entity.HasIndex(a => a.CPF).IsUnique();
            entity.Property(c => c.Email).HasMaxLength(254).IsRequired();
            entity.Property(c => c.Status).HasConversion<string>().HasMaxLength(10);
            entity.Property(c => c.Plan).HasConversion<string>().HasMaxLength(20);
            entity.HasCheckConstraint("CK_Clients_CPF_ValidLength", "[CPF] NOT LIKE '%[^0-9]%' AND LEN([CPF]) = 11");
            entity.HasOne(c => c.Address)
            .WithMany(c => c.Clients)
            .HasForeignKey(c => c.AddressId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Loan>(entity =>
       {
            entity.HasOne(l => l.Client)
           .WithMany(l => l.Loans)
           .HasForeignKey(l => l.ClientId)
           .OnDelete(DeleteBehavior.Restrict);

           entity.HasOne(bc => bc.BookCopy)
           .WithMany(bc => bc.Loans)
           .HasForeignKey(bc => bc.BookCopyId)
           .OnDelete(DeleteBehavior.Restrict);

           entity.HasCheckConstraint("CK_Loan_DueDateAfterLoanDate", "DueDate > LoanDate");
           entity.HasCheckConstraint("CK_Loan_ReturnedDate", "ReturnedDate IS NULL OR ReturnedDate >= LoanDate");


       });
            

    }
}
