using BookShelf.Domain.Entities.Enums;

namespace BookShelf.Domain.Entities;

public class BookCopy
{
    public int BookCopyId { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public required string InventoryCode { get; set; }
    public DateOnly AcquisitionDate { get; set; }
    public PhysicalCondition PhysicalCondition { get; set; }

    public ICollection<Loan> Loans { get; set; } = [];
}
