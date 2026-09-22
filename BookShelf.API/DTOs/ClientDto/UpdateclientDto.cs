using BookShelf.Domain.Entities.Enums;
namespace BookShelf.API.DTOs;

public class UpdateClientDto
{
    public string Name { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public Plan Plan { get; set; }
    public Status Status { get; set; }

    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
}