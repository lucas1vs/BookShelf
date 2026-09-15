namespace BookShelf.Domain.Entities;

public class Address
{
    public int AddressId { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public string? CEP { get; set; }

    public ICollection<Client> Clients { get; set; } = [];
}
