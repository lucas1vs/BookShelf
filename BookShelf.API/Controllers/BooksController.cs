using BookShelf.API.DTOs.BookDto;
using BookShelf.Domain.Entities;
using BookShelf.EFCore;
using BookShelf.EFCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
        // 1. Busca os livros do banco de dados (entidade)
        var books = await _context.Books
            .Include(b => b.Author)
            .ToListAsync();

        // 2. Mapeia a entidade Book para BookDto
        var bookDtos = books.Select(b => new BookDto
        {
            Id = b.BookId,
            Title = b.Title,
            Author = b.Author!.Name

        }).ToList();

        // 3. Retorna a lista de DTOs
        return Ok(bookDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBookById(int id)
    {
        // 1. Busca o livro pelo Id, já trazendo o Author junto
        var book = await _context.Books
            .Include(b => b.Author)
            .SingleOrDefaultAsync(b => b.BookId == id);

        // 2. Se não encontrou, retorna 404
        if (book is null)
        {
            return NotFound();
        }

        // 3. Mapeia pra DTO
        var bookDto = new BookDto
        {
            Id = book.BookId,
            Title = book.Title,
            Author = book.Author!.Name
        };

        // 4. Retorna 200 com o DTO
        return Ok(bookDto);
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookDto createBookDto)
    {
        // 1. Verifica se o autor informado realmente existe
        var author = await _context.Authors
            .SingleOrDefaultAsync(a => a.AuthorId == createBookDto.AuthorId);

        if (author is null)
        {
            return BadRequest($"Autor com Id {createBookDto.AuthorId} não encontrado.");
        }

        // 2. Cria o Book usando a navegação (já que agora você TEM o objeto Author em mãos)
        var book = new Book
        {
            Author = author,
            Title = createBookDto.Title,
            Genre = createBookDto.Genre,
            Pages = createBookDto.Pages,
            Synopsis = createBookDto.Synopsis,
            YearOfPublication = createBookDto.YearOfPublication
        };

        // 3. Adiciona e salva
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // 4. Mapeia pro DTO de resposta — agora book.Author NÃO é null,
        // porque foi você mesmo quem atribuiu o objeto author buscado do banco
        var bookDto = new BookDto
        {
            Id = book.BookId,
            Title = book.Title,
            Author = book.Author.Name
        };

        // 5. Retorna 201
        return CreatedAtAction(nameof(GetBookById), new { id = bookDto.Id }, bookDto);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
    {
        // 1. Busca o livro a ser atualizado
        var book = await _context.Books
            .SingleOrDefaultAsync(b => b.BookId == id);

        if (book is null)
        {
            return NotFound();
        }

        // 2. Valida se o novo autor informado existe no banco de dados
        var author = await _context.Authors
            .SingleOrDefaultAsync(a => a.AuthorId == updateBookDto.AuthorId);

        if (author is null)
        {
            return BadRequest($"Autor com Id {updateBookDto.AuthorId} não encontrado.");
        }

        // 3. Atualiza as propriedades do livro
        book.Author = author;
        book.Title = updateBookDto.Title;
        book.Genre = updateBookDto.Genre;
        book.Pages = updateBookDto.Pages;
        book.Synopsis = updateBookDto.Synopsis;
        book.YearOfPublication = updateBookDto.YearOfPublication;

        // 4. Salva as alterações no banco de dados
        await _context.SaveChangesAsync();

        // 5. Retorna sucesso sem conteúdo
        return NoContent();
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books
             .SingleOrDefaultAsync(a => a.BookId == id);

        if (book is null)
        {
            return NotFound();
        }
        var temcopias = await _context.BookCopies.AnyAsync(b => b.BookId == id);

        if (temcopias)
        {
            return BadRequest("Não é possível apagar este Libro pois ele possui Cópias cadastrados.");
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return NoContent();

    }
}
        