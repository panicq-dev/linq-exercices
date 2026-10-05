using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LINQ_Exercices.Models;

public class Cliente
{
    
    public int Id { get; set; }
    [MaxLength(250)]
    public required string Nome { get; set; }
    [MaxLength(150)]
    public required string Email { get; set; }
    public required IEnumerable<Pedido> Pedidos { get; set; }
}