using DragoesEscola.Pages;
using System.ComponentModel.DataAnnotations.Schema;

namespace DragoesEscola.Models
{
  public class InviteDto
  {
    public string EventId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
  }

  public class InviteModel
  {
    public int id { get; set; }
    public int EventId { get; set; }
    public EventModel Event { get; set; }
    public int UserId { get; set; }
    public UserModel User { get; set; }
    public bool Compareceu { get; set; }
    public DateTime? HoraChegada { get; set; }
  }

  public class EventModel
  {
    public int id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Cep { get; set; }
    public string Endereco { get; set; }
    public string Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public int Tipo { get; set; }
    public bool PublicEvent { get; set; }
    public string Codigo { get; set; }
  }

  public class UserModel
  {
    public int Id { get; set; }
    public int n_carteirinha { get; set; } = 0;
    public string nome { get; set; } = string.Empty;
    public string rg { get; set; } = string.Empty;
    public string departamento { get; set; } = string.Empty;
    public DateTime data_nascimento { get; set; }
    public string telefone { get; set; } = string.Empty;
    public decimal valor { get; set; }
    public bool pago { get; set; }
    public string forma_pgto { get; set; } = string.Empty;
    public DateTime? data_pagamento { get; set; }
    public string acompanhante_de { get; set; } = string.Empty;
    public DateTime? data_retirada { get; set; }
    public string fechamento { get; set; } = string.Empty;
    public string foto { get; set; } = string.Empty;
    public string eMail { get; set; } = string.Empty;
    public string LoginPass { get; set; } = string.Empty;
    public string tokenPassword { get; set; } = string.Empty;
    public DateTime? requestDate { get; set; }
    public bool isAdmin { get; set; } = false;

    [NotMapped]
    public string baseUrl { get; set; } = string.Empty;
    [NotMapped]
    public string ConfirmLoginPass { get; set; } = string.Empty;
    [NotMapped]
    public List<string> Departamentos { get; set; } = new List<string>();
  }
}
