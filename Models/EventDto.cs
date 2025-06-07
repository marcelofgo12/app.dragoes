namespace DragoesEscola.Models
{
  public class EventDto
  {
    public int? id { get; set; } = 0;
    public string? Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; } = string.Empty;
    public string? Cep { get; set; } = string.Empty;
    public string? Endereco { get; set; } = string.Empty;
    public string? Numero { get; set; } = string.Empty;
    public string? Bairro { get; set; } = string.Empty;
    public string? Cidade { get; set; } = string.Empty;
    public string? Estado { get; set; } = string.Empty;
    public DateTime? DataInicio { get; set; } = default(DateTime?);
    public DateTime? DataFim { get; set; }= DateTime.Now;
    public int? Tipo { get; set; } = 0;
    public bool? PublicEvent { get; set; } = false;
    public string? Codigo { get; set; } = string.Empty;
  }
}
