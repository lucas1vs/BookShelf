namespace BookShelf.API.DTOs.LoanDto;

public class LoanDto
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int BookCopyId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public DateOnly LoanDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly? ReturnedDate { get; set; }


}
