using BookShelf.Domain.Entities.Enums;

namespace BookShelf.API.DTOs.BookCopyDto
{
    public class UpdateBookCopyDto
    {
        public PhysicalCondition PhysicalCondition { get; set; }
        public bool IsAvailable { get; set; }
    }
}
