using BookShelf.Domain.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.API.DTOs.BookCopyDto;

[Route("api/[controller]")]
[ApiController]
public class BookCopyDto 
{
   public int Id { get; set; }
    public string InventoryCode { get; set; } = string.Empty;
    public DateOnly AcquisitionDate { get; set; }
    public PhysicalCondition PhysicalCondition { get; set; }
    public bool IsAvailable { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
}
