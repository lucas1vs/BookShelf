using BookShelf.Domain.Entities.Enums;

namespace BookShelf.API.DTOs;

public class CreateBookCopyDto
{
    public int BookId { get; set; }
    public string InventoryCode { get; set; }
    public DateOnly AcquisionDate { get; set; }
    public PhysicalCondition PhysicalCondition { get; set; }
}
