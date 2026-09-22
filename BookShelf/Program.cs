
using BookShelf.EFCore.Data;
using BookShelf.EFCore.Seed;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptions<AppDbContext>();
using var context = new AppDbContext(options);

DataSeeder.Seed(context);
Console.WriteLine("Seed atualizada com sucesso");
Console.ReadKey(); 

