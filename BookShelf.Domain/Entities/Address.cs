namespace BookShelf.Domain.Entities;

public class Address
{
    public int AddressId { get; set; }
    public required string Country { get; set; }
    public required string State { get; set; }
    public required string City { get; set; }
    public required string Neighborhood { get; set; }
    public required string CEP { get; set; }

    public ICollection<Client>? Clients { get; set; } ////1:N relação com Address ( N)
}
