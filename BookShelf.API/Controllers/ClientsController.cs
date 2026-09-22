using BookShelf.API.DTOs;
using BookShelf.API.DTOs.AuthorDto;
using BookShelf.API.DTOs.ClientDto;
using BookShelf.Domain.Entities;
using BookShelf.Domain.Entities.Enums;
using BookShelf.EFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
namespace BookShelf.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
    {
        // 1. Busca os Clients do banco de dados (entidade)
        var clients = await _context.Clients
            .Include(c => c.Address)
            .ToListAsync();

        // 2. Mapeia a entidade Client para ClientDto
        var clientDto = clients.Select(b => new ClientDto
        {
            Id = b.ClientId,
            Name = b.Name,
            Gender = b.Gender,
            Status = b.Status,
            Plan = b.Plan,
            Country = b.Address!.Country,
            City = b.Address.City,
            Neighborhood = b.Address.Neighborhood

        }).ToList();


        // 3. Retorna a lista de DTOs
        return Ok(clientDto);
    }
    [HttpGet("{id}")]

    public async Task<ActionResult<ClientDto>> GetClientById(int id)
    {
        // 1. Busca o Client pelo Id, já trazendo o Address junto
        var client = await _context.Clients
            .Include(b => b.Address)
            .SingleOrDefaultAsync(b => b.ClientId == id);

        // 2. Se não encontrou, retorna 404
        if (client is null)
        {
            return NotFound();
        }

        // 3. Mapeia pra DTO
        var clientDto = new ClientDto
        {
            Id = client.ClientId,
            Name = client.Name,
            Gender = client.Gender,
            Status = client.Status,
            Plan = client.Plan,
            Country = client.Address!.Country,
            City = client.Address!.City,
            Neighborhood = client.Address!.Neighborhood
        };

        // 4. Retorna 200 com o DTO
        return Ok(clientDto);
    }


    [HttpPost]

    public async Task<ActionResult<ClientDto>> CreateClient([FromBody] CreateClientDto createClientDto)
    {
        // 1. Cria o Address a partir dos dados recebidos (sem buscar nada)
        var address = new Address
        {
            Country = createClientDto.Country,
            State = createClientDto.State,
            City = createClientDto.City,
            Neighborhood = createClientDto.Neighborhood,
            CEP = createClientDto.CEP
        };

        // 2. Cria o Client, usando navegação pro Address + valores fixados no código
        var client = new Client
        {
            Address = address,
            Name = createClientDto.Name,
            Gender = createClientDto.Gender,
            CPF = createClientDto.CPF,
            DateOfBirth = createClientDto.DateOfBirth,
            Email = createClientDto.Email,
            Plan = createClientDto.Plan,
            Status = Status.Active,
            DateOfRegister = DateTime.Now
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        var clientDto = new ClientDto
        {
            Id = client.ClientId,
            Name = client.Name,
            Gender = client.Gender,
            Status = client.Status,
            Plan = client.Plan,
            Country = client.Address.Country,
            City = client.Address.City,
            Neighborhood = client.Address.Neighborhood
        };


        return CreatedAtAction(nameof(GetClientById),
         new
         {
             id = clientDto.Id
         }, clientDto);

        

    }
    [HttpPut("{id}")]

    public async Task<ActionResult> UpdateClient(int id, [FromBody] UpdateClientDto updateClientDto)

    {
        var client = await _context.Clients
          .Include(c => c.Address)
        .SingleOrDefaultAsync(c => c.ClientId == id);

        if (client is null)
        {
            return NotFound();
        }

        client.Name = updateClientDto.Name;
        client.Gender = updateClientDto.Gender;
        client.DateOfBirth = updateClientDto.DateOfBirth;
        client.Email = updateClientDto.Email;
        client.Plan = updateClientDto.Plan;
        client.Status = updateClientDto.Status;

        client.Address!.Country = updateClientDto.Country;
        client.Address.State = updateClientDto.State;
        client.Address.City = updateClientDto.City;
        client.Address.Neighborhood = updateClientDto.Neighborhood;
        client.Address.CEP = updateClientDto.CEP;

        await _context.SaveChangesAsync();
        return NoContent();


    }


    [HttpDelete("{id}")]

    public async Task<ActionResult> DeletedClient(int id)
    {
        var client = await _context.Clients
             .Include(c => c.Address)
          .SingleOrDefaultAsync(c => c.ClientId == id);

        if (client is null)
        {
            return NotFound();
        }

     

        // 3. Remove o objeto inteiro do contexto
        _context.Clients.Remove(client);

        // 4. Salva as alterações no banco de dados
        await _context.SaveChangesAsync();

        // 5. Retorna sucesso sem conteúdo
        return NoContent();
    }


}   
    

       

    

