using BookShelf.Domain.Entities.Enums;

namespace BookShelf.Domain.Entities;

public class Author
{
    public int AuthorId { get; set; }// chave primaria
    public required string Name { get; set; }
    public string? Biography { get; set; }
    public Gender Gender { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>(); // 1:N relação com Book (N)
}
