using BookShelf.Domain.Entities;
using BookShelf.Domain.Entities.Enums;

namespace BookShelf.API.DTOs.BookDto;

public class CreateBookDto
{
    public int AuthorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public BookGenre Genre { get; set; }
    public int Pages { get; set; }
    public string Synopsis { get; set; } = string.Empty;
    public int YearOfPublication { get; set; }


}
