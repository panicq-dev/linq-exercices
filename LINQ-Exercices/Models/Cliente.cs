namespace LINQ_Exercices.Models;

public class Cliente
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required IEnumerable<Pedido> Pedidos { get; set; }
}