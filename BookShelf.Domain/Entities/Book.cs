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
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;

    public ICollection<BookCopy> Copies { get; set; } = [];
}
