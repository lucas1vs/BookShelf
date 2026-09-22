using BookShelf.API.DTOs.AuthorDto;
using BookShelf.Domain.Entities;
using BookShelf.EFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthorsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
    {
        // 1.Busca os Autores do banco de dados(entidade)
        var autores = await _context.Authors
             .ToListAsync();

        // 2. Mapeia a entidade Client para ClientDto
        var authorDto = autores.Select(a => new AuthorDto
        {
            Id = a.AuthorId,
            Name = a.Name,
            Gender = a.Gender



        }).ToList();

        // 3. Retorna a lista de DTOs
        return Ok(authorDto);

    }

    [HttpGet("{id}")]

    public async Task<ActionResult<AuthorDto>> GetAuthorById(int id)
    {
        var autores = await _context.Authors
            .SingleOrDefaultAsync(a => a.AuthorId == id);

        if (autores is null)
        {
            return NotFound();
        }

        // 3. Mapeia pra DTO
        var authorDto = new AuthorDto
        {
            Id = autores.AuthorId,
            Name = autores.Name,
            Gender = autores.Gender

        };

        // 4. Retorna 200 com o DTO
        return Ok(authorDto);

    }

    [HttpPost]
    public async Task<ActionResult<AuthorDto>> CreateAuthor([FromBody] CreateAuthorDto createAuthorDto)
    {
        // 1. Converte o DTO recebido numa entidade Author
        var author = new Author
        {
            Name = createAuthorDto.Name,
            Gender = createAuthorDto.Gender,
            Biography = createAuthorDto.Biography
        };

        // 2. Adiciona ao contexto e salva no banco
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        // 3. Mapeia a entidade (já com Id gerado) pro DTO de resposta
        var authorDto = new AuthorDto
        {
            Id = author.AuthorId,
            Name = author.Name,
            Gender = author.Gender
        };

        // 4. Retorna sucesso
        return CreatedAtAction(nameof(GetAuthorById), new { id = authorDto.Id }, authorDto);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDto updateAuthorDto)
    {
        var author = await _context.Authors
            .SingleOrDefaultAsync(a => a.AuthorId == id);

        if (author is null)
        {
            return NotFound();
        }

        author.Name = updateAuthorDto.Name;
        author.Gender = updateAuthorDto.Gender;
        author.Biography = updateAuthorDto.Biography;

        // 4. Salva as alterações no banco de dados
        await _context.SaveChangesAsync();

        // 5. Retorna o status 204 (No Content) indicando sucesso sem corpo de resposta
        return NoContent();



    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var autor = await _context.Authors
             .SingleOrDefaultAsync(a => a.AuthorId == id);

        if (autor is null)
        {
            return NotFound();
        }
        var temLivros = await _context.Books.AnyAsync(b => b.AuthorId == id);

        if (temLivros)
        {
            return BadRequest("Não é possível apagar este autor pois ele possui livros cadastrados.");
        }


        // 3. Remove o objeto inteiro do contexto
        _context.Authors.Remove(autor);

        // 4. Salva as alterações no banco de dados
         await _context.SaveChangesAsync();

        // 5. Retorna sucesso sem conteúdo
        return NoContent();
    }

   

}       
