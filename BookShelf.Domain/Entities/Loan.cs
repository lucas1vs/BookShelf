using BookShelf.Domain.Entities.Enums;

namespace BookShelf.Domain.Entities;

public class Loan
{
    public int LoanId { get; set; }
    public int ClientId { get; set; } // Chave estrangeira
    public Client Client { get; set; } // propriedade de navegação
    public int BookCopyId { get; set; } // Chave estrangeira
    public BookCopy BookCopy { get; set; }
    public Status Status { get; set; }
    public DateOnly LoanDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly? ReturnedDate { get; set; }


    public void Validate()
    {
        // Validação de negócio: Data de devolução não pode ser anterior à data do empréstimo
        if (ReturnedDate.HasValue && ReturnedDate.Value < LoanDate)
        {
            throw new InvalidOperationException("A data de devolução não pode ser anterior à data do empréstimo.");
        }

        if (DueDate < LoanDate)
        {
            throw new InvalidOperationException("A data de vencimento não pode ser anterior à data do empréstimo.");
        }

    }

}

