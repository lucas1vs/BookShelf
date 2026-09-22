using BookShelf.Domain.Entities.Enums;

namespace BookShelf.Domain.Entities;

public class Book
{
    public int BookId { get; set; }
    public required string Title { get; set; }
    public BookGenre Genre { get; set; }
    public int Pages { get; set; }
    public string? Synopsis { get; set; }
    public int YearOfPublication { get; set; }

    public int AuthorId { get; set; } // Chave estrangeira 
    public Author? Author { get; set; } // 1:N relação com Author (1)

    public ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();
}
