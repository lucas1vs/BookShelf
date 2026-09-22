using BookShelf.Domain.Entities.Enums;

namespace BookShelf.Domain.Entities;

public class BookCopy
{
    public int BookCopyId { get; set; }
    public int BookId { get; set; } // chave estrangeira
    public  required Book Book { get; set; } 
    public required string InventoryCode { get; set; }
    public DateOnly AcquisitionDate { get; set; }
    public PhysicalCondition PhysicalCondition { get; set; }
    public bool IsAvailable { get; set; } = true;

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
