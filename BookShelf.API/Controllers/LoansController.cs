using BookShelf.API.DTOs.LoanDto;
using BookShelf.Domain.Entities;
using BookShelf.EFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly AppDbContext _context;

    public LoansController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<LoanDto>>> GetLoans()
    {
        // 1. Busca os Empréstimos do banco de dados (entidade)
        var loans = await _context.Loans
            .Include(l => l.Client)
            .Include(l => l.BookCopy)
            .ThenInclude(bc => bc.Book)
            .ToListAsync();

        // 2. Mapeia a entidade Loan para LoanDto
        var loanDto = loans.Select(l => new LoanDto
        {
            Id = l.LoanId,
            ClientId = l.ClientId,
            ClientName = l.Client.Name,
            BookCopyId = l.BookCopyId,
            BookTitle = l.BookCopy.Book.Title,
            LoanDate = l.LoanDate,
            DueDate = l.DueDate,
            ReturnedDate = l.ReturnedDate


        }).ToList();

        // 3. Retorna a lista de DTOs
        return Ok(loanDto);

        
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<LoanDto>> GetLoanById(int id)
    {
        // 1. Busca o Loan pelo Id, já trazendo o Client / Book / BookyCopy junto
        var loan = await _context.Loans
            .Include(l => l.Client)
            .Include(l => l.BookCopy)
            .ThenInclude(bc => bc.Book)
            .SingleOrDefaultAsync(l => l.LoanId == id);

        // 2. Se não encontrou, retorna 404
        if (loan is null)
        {
            return NotFound();
        }

        // 3. Mapeia pra DTO
        var loanDto = new LoanDto
        {
            Id = loan.LoanId,
            ClientId = loan.ClientId,
            ClientName = loan.Client.Name,
            BookCopyId = loan.BookCopyId,
            BookTitle = loan.BookCopy.Book.Title,
            LoanDate = loan.LoanDate,
            DueDate = loan.DueDate,
            ReturnedDate = loan.ReturnedDate

        };

        // 4. Retorna 200 com o DTO
        return Ok(loanDto);
    }

    [HttpPost]
    public async Task<ActionResult<LoanDto>> CreateLoan([FromBody] CreateLoanDto createLoanDto)
    {
        // 1. Valida se o Client existe
        var client = await _context.Clients
            .SingleOrDefaultAsync(l => l.ClientId == createLoanDto.ClientId);

        if (client is null)
        {
            return BadRequest($"Client com Id {createLoanDto.ClientId} não encontrado.");
        }

        // 2. Valida se o BookCopy existe (com o Book incluído, pra usar o Title depois)
        var bookCopy = await _context.BookCopies
            .Include(bc => bc.Book)
            .SingleOrDefaultAsync(bc => bc.BookCopyId == createLoanDto.BookCopyId);

        if (bookCopy is null)
        {
            return BadRequest($"BookCopy com Id {createLoanDto.BookCopyId} não encontrado.");
        }

        // 3. Cria o Loan usando navegação + datas calculadas pelo código
        var loanDate = DateOnly.FromDateTime(DateTime.Now);

        var loan = new Loan
        {
            Client = client,
            BookCopy = bookCopy,
            LoanDate = loanDate,
            DueDate = loanDate.AddDays(15)
        };

        // 4. Adiciona e salva
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();

        // 5. Mapeia pro DTO de resposta
        var loanDto = new LoanDto
        {
            Id = loan.LoanId,
            ClientId = client.ClientId,
            ClientName = client.Name,
            BookCopyId = bookCopy.BookCopyId,
            BookTitle = bookCopy.Book.Title,
            LoanDate = loan.LoanDate,
            DueDate = loan.DueDate,
            ReturnedDate = loan.ReturnedDate
        };

        // 6. Retorna 201
        return CreatedAtAction(nameof(GetLoanById), new { id = loanDto.Id }, loanDto);
    }

    [HttpPut("{id}")]

    public async Task<ActionResult> UpdateLoans(int id, [FromBody] UpdateLoanDto updateLoanDto)
    {
        var loan = await _context.Loans
         .Include(l => l.BookCopy)
        .SingleOrDefaultAsync(l => l.LoanId == id);

        if (loan is null)
        {
            return NotFound();
        }

        loan.ReturnedDate = updateLoanDto.ReturnedDate;
        loan.BookCopy!.IsAvailable = true;

        await _context.SaveChangesAsync();
        return NoContent();


    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeletedLoans(int id)
    {
        var loan = await _context.Loans
         .Include(l => l.BookCopy)
        .SingleOrDefaultAsync(l => l.LoanId == id);

        if (loan is null)
        {
            return NotFound(); 
        }


        // 3. Remove o objeto inteiro do contexto
        _context.Loans.Remove(loan);

        // 4. Salva as alterações no banco de dados
        await _context.SaveChangesAsync();

        // 5. Retorna sucesso sem conteúdo
        return NoContent();

    }

}
