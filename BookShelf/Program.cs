
using BookShelf.EFCore.Data;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptions<AppDbContext>();
using var context = new AppDbContext(options);

context.Database.EnsureDeleted();
Console.WriteLine("Banco apagado.");

context.Database.EnsureCreated();
Console.WriteLine("Banco criado.");

Console.ReadKey();
context.SaveChanges();
