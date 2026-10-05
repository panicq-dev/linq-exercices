namespace LINQ_Exercices.Data.DTO;

public class PedidoDto
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public int ClienteId { get; set; }
    public int Valor { get; set; }
    public bool Status { get; set; }
}