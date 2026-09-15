using BookShelf.Domain.Entities.Enums;

namespace BookShelf.Domain.Entities;

public class Client
{
    public int ClientId { get; set; }
    public required string Name { get; set; }
    public Gender Gender { get; set; }
    public required string CPF { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public required string Email { get; set; }
    public DateTime DateOfRegister { get; set; }
    public Status Status { get; set; }
    public Plan Plan { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; } = null!;

    public ICollection<Loan> Loans { get; set; } = [];
}
