using BookShelf.API.DTOs;
using BookShelf.API.DTOs.AuthorDto;
using BookShelf.API.DTOs.BookCopyDto;
using BookShelf.Domain.Entities;
using BookShelf.Domain.Entities.Enums;
using BookShelf.EFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookCopiesController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookCopiesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookCopyDto>>> GetBookCopies()
    {
        // 1. Busca os BookCopies do banco de dados (entidade)
        var bookCopies = await _context.BookCopies
            .Include(c => c.Book)
            .ToListAsync();

        // 2. Mapeia a entidade BookCopy para BookCopyDto
        var bookCopyDto = bookCopies.Select(b => new BookCopyDto
        {
            Id = b.BookCopyId,
            BookId = b.BookId,
            InventoryCode = b.InventoryCode,
            AcquisitionDate = b.AcquisitionDate,
            PhysicalCondition = b.PhysicalCondition,
            IsAvailable = b.IsAvailable,
            BookTitle = b.Book!.Title

        }).ToList();

        // 3. Retorna a lista de DTOs
        return Ok(bookCopyDto);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<BookCopyDto>> GetBookCopyById(int id)
    {
        // 1. Busca o BookCopy pelo Id, já trazendo o Book junto (Include)
        var bookCopy = await _context.BookCopies
            .Include(c => c.Book)
            .SingleOrDefaultAsync(b => b.BookCopyId == id);

        // 2. Se não encontrou, retorna 404
        if (bookCopy is null)
        {
            return NotFound();
        }

        // 3. Mapeia pra DTO
        var bookCopyDto = new BookCopyDto
        {
            Id = bookCopy.BookCopyId,
            BookId = bookCopy.BookId,
            InventoryCode = bookCopy.InventoryCode,
            AcquisitionDate = bookCopy.AcquisitionDate,
            PhysicalCondition = bookCopy.PhysicalCondition,
            IsAvailable = bookCopy.IsAvailable,
            BookTitle = bookCopy.Book!.Title
        };

        // 4. Retorna 200 com o DTO
        return Ok(bookCopyDto);
    }

    [HttpPost]
    public async Task<ActionResult<BookCopyDto>> CreateBookCopy([FromBody] CreateBookCopyDto createBookCopyDto)
    {
        // 1. Verifica se o livro informado realmente existe
        var book = await _context.Books
            .SingleOrDefaultAsync(b => b.BookId == createBookCopyDto.BookId);

        if (book is null)
        {
            return BadRequest($"Book com Id {createBookCopyDto.BookId} não encontrado.");
        }

        // 2. Cria o BookCopy usando a navegação (já que agora você TEM o objeto Book em mãos)
        var bookCopy = new BookCopy
        {
            Book = book,
            InventoryCode = createBookCopyDto.InventoryCode,
            AcquisitionDate = createBookCopyDto.AcquisionDate,
            PhysicalCondition = createBookCopyDto.PhysicalCondition,
            IsAvailable = true
        };

       
        // 3. Adiciona e salva
        _context.BookCopies.Add(bookCopy);
        await _context.SaveChangesAsync();

        // 4. Mapeia pro DTO de resposta — agora bookCopy.Book NÃO é null,
        // porque foi você mesmo quem atribuiu o objeto book buscado do banco
        var bookCopyDto = new BookCopyDto
        {
            Id = bookCopy.BookCopyId,
            InventoryCode = bookCopy.InventoryCode,
            AcquisitionDate = bookCopy.AcquisitionDate,
            PhysicalCondition = bookCopy.PhysicalCondition,
            IsAvailable = bookCopy.IsAvailable,
            BookId = bookCopy.BookId,
            BookTitle = bookCopy.Book.Title
        };

        // 5. Retorna 201
        return CreatedAtAction(nameof(GetBookCopyById), new { id = bookCopyDto.Id }, bookCopyDto);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateBookCopy(int id, [FromBody] UpdateBookCopyDto updateBookCopyDto)
    {
        var bookcopies = await _context.BookCopies
          .SingleOrDefaultAsync(bc => bc.BookCopyId == id);

        if (bookcopies is null)
        {
            return NotFound();
        }

        bookcopies.PhysicalCondition = updateBookCopyDto.PhysicalCondition;
        bookcopies.IsAvailable = updateBookCopyDto.IsAvailable;


        await _context.SaveChangesAsync();
        return NoContent();

    }



    [HttpDelete("{id}")]

    public async Task<ActionResult> DeletedBookCopy(int id)
    {
        var bookcopy = await _context.BookCopies
            .SingleOrDefaultAsync(a => a.BookCopyId == id);

        if (bookcopy is null)
        {
            return NotFound();
        }

        var temloan = await _context.Loans.AnyAsync(a => a.BookCopyId == id);

        if (temloan)
        {
            return BadRequest("Não é possível apagar esta Cópia pois ele possui Empréstimos cadastrados.");
        }

        _context.BookCopies.Remove(bookcopy);
        await _context.SaveChangesAsync();
        return NoContent();

    }

}