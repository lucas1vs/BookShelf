using BookShelf.Domain.Entities.Enums;

namespace BookShelf.API.DTOs;

public class CreateClientDto
{
  
    public string Name { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string CPF { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public Plan Plan { get; set; }
    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
}