namespace LINQ_Exercices.Models;

public class Pedido
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public int ClienteId { get; set; }
    public int Valor { get; set; }
    public bool Status { get; set; }
    public required Cliente Cliente { get; set; }
}