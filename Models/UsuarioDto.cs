using System.ComponentModel.DataAnnotations;

namespace DragoesEscola.Models
{
  public class UsuarioViewModel()
  {
    public List<UsuarioDto> usuarioList { get; set; } = new List<UsuarioDto>();
    public List<string> departamentos { get; set; } = new List<string>();
  }

  public class UsuarioDto
  {
    public int Id { get; set; }
    public int n_carteirinha { get; set; } = 0;
    public string? nome { get; set; } = string.Empty;
    public string? rg { get; set; } = string.Empty;
    public string departamento { get; set; } = string.Empty;
    public DateTime? data_nascimento { get; set; }
    public string? telefone { get; set; } = string.Empty;
    public decimal valor { get; set; }
    public bool pago { get; set; }
    public string forma_pgto { get; set; } = string.Empty;
    public DateTime? data_pagamento { get; set; } = new DateTime?();
    public string acompanhante_de { get; set; } = string.Empty;
    public DateTime? data_retirada { get; set; }
    public string fechamento { get; set; } = string.Empty;
    public string? foto { get; set; } = string.Empty;
    public string? LoginPass { get; set; } = string.Empty;
    public string ConfirmLoginPass { get; set; } = string.Empty;
    public string tokenPassword { get; set; } = string.Empty;
    public DateTime? requestDate { get; set; }
    public string baseUrl { get; set; } = string.Empty;
    public bool isAdmin { get; set; } = false;
    public bool isScanner { get; set; } = false;
    public List<EventDto> UserEvents { get; set; } = new List<EventDto>();
  }
}
