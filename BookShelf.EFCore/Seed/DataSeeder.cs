using BookShelf.Domain.Entities;
using BookShelf.Domain.Entities.Enums;
using BookShelf.EFCore.Data;

namespace BookShelf.EFCore.Seed;

public class DataSeeder
{
    public static void Seed(AppDbContext context)
    {
        // Se já existem cópias, o seeder já rodou. Para aqui e evita duplicadas!
        if (context.Loans.Any())
        {
            return;
        }

        var autores = new List<Author>
        {
            new Author
            {
                Name = "Machado de Assis",
                Biography = "Biografia do Machado...",
                Gender = Gender.Male,
            },
            new Author
            {
                Name = "Ernest Hemingway",
                Biography = "Biografia do Hemingway...",
                Gender = Gender.Male,
            }
        };
        context.Authors.AddRange(autores);

        var livros = new List<Book>
        {
            new Book
            {
                Title = "Dom casmurro",
                Genre = BookGenre.Romance,
                Pages = 302,
                Synopsis = "teste",
                YearOfPublication = 1899,
                Author = autores[0]
            },
            new Book
            {
                Title = "o velho e o mar",
                Genre = BookGenre.Fiction,
                Pages = 126,
                Synopsis = "Teste2",
                YearOfPublication = 1952,
                Author = autores[1]
            }
        };
        context.Books.AddRange(livros); // Obrigatório para o EF Core rastrear a lista

        var enderecos = new List<Address>
        {
            new Address
            {
                Country = "Brasil",
                State = "SP",
                City = "Graal",
                Neighborhood = "Rio das ostras",
                CEP = "12345678"
            }
        };
        context.Addresses.AddRange(enderecos);

        var clientes = new List<Client>
        {
            new Client
            {
                Name = "Artur dias",
                Gender = Gender.Male,
                CPF = "12345678914",
                DateOfBirth = new DateOnly(2006,03,28),
                Email = "Teste@gmail.com",
                DateOfRegister = new DateTime(2025,09,28),
                Status = Status.Active,
                Plan = Plan.IndividualPlan,
                Address = enderecos[0]
            },
            new Client
            {
                Name = "maria",
                Gender = Gender.Female,
                CPF = "12345678915",
                DateOfBirth = new DateOnly(2006,03,28),
                Email = "teste2@gmail.com",
                DateOfRegister = new DateTime(2025,09,28),
                Status = Status.Active,
                Plan = Plan.IndividualPlan,
                Address = enderecos[0]
            }
        };
        context.Clients.AddRange(clientes); // Obrigatório

        var copias = new List<BookCopy>
        {
            new BookCopy
            {
                Book = livros.First(),
                InventoryCode = "código teste01",
                AcquisitionDate = new DateOnly(2025,09,28),
                PhysicalCondition = PhysicalCondition.New,
                IsAvailable = true
            }
        };
        context.BookCopies.AddRange(copias); // Obrigatório

        var emprestimos = new List<Loan>
        {
            new Loan
            {
                Client = clientes[0],
                BookCopy = copias[0],
                LoanDate = DateOnly.FromDateTime(DateTime.Today),
                DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(7)),
                Status = Status.Active
                
        
            }
            
        };
            context.Loans.AddRange(emprestimos);
        context.SaveChanges();
    }
}