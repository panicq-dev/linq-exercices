namespace LINQ_Exercices.Models;

public class Pedido
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public int ClienteId { get; set; }
    public required Cliente Cliente { get; set; }
}