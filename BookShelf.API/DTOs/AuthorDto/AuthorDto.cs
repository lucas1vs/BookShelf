using BookShelf.Domain.Entities.Enums;

namespace BookShelf.API.DTOs.AuthorDto;

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Gender Gender { get; set; }
}
