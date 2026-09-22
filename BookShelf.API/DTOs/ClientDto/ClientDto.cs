using BookShelf.Domain.Entities.Enums;

namespace BookShelf.API.DTOs.ClientDto;

 public class ClientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public Status Status { get; set; }
    public Plan Plan { get; set; }
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;

}

