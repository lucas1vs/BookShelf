namespace BookShelf.Domain.Entities;

public class Loan
{
    public int LoanId { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public int BookCopyId { get; set; }
    public BookCopy BookCopy { get; set; } = null!;
    public DateOnly LoanDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly? ReturnedDate { get; set; }
}
