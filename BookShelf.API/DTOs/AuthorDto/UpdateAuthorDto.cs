using BookShelf.Domain.Entities.Enums;

namespace BookShelf.API.DTOs.AuthorDto;

public class UpdateAuthorDto
{
    public string Name { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string Biography { get; set; } = string.Empty;
}
