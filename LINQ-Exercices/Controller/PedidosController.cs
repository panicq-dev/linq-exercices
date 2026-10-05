using LINQ_Exercices.Data;
using LINQ_Exercices.Data.DTO;
using LINQ_Exercices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LINQ_Exercices.Controller;

[ApiController]
[Route("[controller]")]
public class PedidosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PedidosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CriarPedido(PedidoDto dto)
    {
        var cliente = await _context.Clientes.FindAsync(dto.ClienteId);
        if (cliente is null)
        {
            return BadRequest($"Cliente com id {dto.ClienteId} não encontrado.");
        }

        var pedido = new Pedido
        {
            Data = dto.Data,
            ClienteId = dto.ClienteId,
            Cliente = cliente
        };

        await _context.Pedidos.AddAsync(pedido);
        await _context.SaveChangesAsync();

        return Ok(new PedidoDto
        {
            Id = pedido.Id,
            Data = pedido.Data,
            ClienteId = pedido.ClienteId
        });
    }

    [HttpGet]
    public async Task<IEnumerable<PedidoDto>> BuscarPedidos()
    {
        return await _context.Pedidos
            .Select(p => new PedidoDto
            {
                Id = p.Id,
                Data = p.Data,
                ClienteId = p.ClienteId
            })
            .OrderBy(p => p.Data)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PedidoDto>> BuscarPedidoPorId(int id)
    {
        var pedido = await _context.Pedidos
            .Where(p => p.Id == id)
            .Select(p => new PedidoDto
            {
                Id = p.Id,
                Data = p.Data,
                ClienteId = p.ClienteId,
                Valor =  p.Valor,
                Status = p.Status
            })
            .FirstOrDefaultAsync();

        if (pedido is null)
        {
            return NotFound();
        }

        return pedido;
    }

    [HttpGet("maior-valor")]
    public async Task<ActionResult<PedidoDto>> MaiorValor()
    {
        var pedidoMax = await _context.Pedidos
            .OrderByDescending(p => p.Valor)
            .Select(p => new PedidoDto()
            {
                Id = p.Id,
                Data = p.Data,
                Valor = p.Valor,
                Status = p.Status,
                ClienteId = p.ClienteId
            }).FirstOrDefaultAsync();
        if (pedidoMax is null)
        {
            return BadRequest("Erro: todos os pedidos tem o mesmo valor, ou não existe nenhum pedido.");
        }
        return Ok(pedidoMax);
    }

    [HttpGet("total")]
    public async Task<IActionResult> ValorTotal()
    {
        var quantidade = await _context.Pedidos.CountAsync();
        var total = await _context.Pedidos.SumAsync(p => p.Valor);
        return Ok(new
        {
            ValorTotal = total,
            Quantidade = quantidade,
        });
    }
}