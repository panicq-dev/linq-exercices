using AutoMapper;
using LINQ_Exercices.Data;
using LINQ_Exercices.Data.DTO;
using LINQ_Exercices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LINQ_Exercices.Controller;

[ApiController]
[Route("[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ClientesController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CriarCliente(ClienteDto dto)
    {
        var cliente = _mapper.Map<Cliente>(dto);
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
        return Ok(cliente);
    }

    [HttpGet]
    public async Task<IEnumerable<ClienteDto>> BuscarClientes()
    {
        var clientes = await _context.Clientes
            .Select(c => new ClienteDto
            {
                Email = c.Email,
                Nome = c.Nome
            })
            .OrderBy(c => c.Nome)
            .ToListAsync();
        return clientes;
    }

    [HttpGet("{id}")]
    public async Task<ClienteDto> BuscarClientePorId(int id)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        var clienteDto = _mapper.Map<ClienteDto>(cliente);
        return clienteDto;
    }
}