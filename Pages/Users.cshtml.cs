using DragoesEscola.Models;
using DragoesEscola.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DragoesEscola.Pages
{
  [IgnoreAntiforgeryToken]
  public class UsersModel : PageModel
  {
    [BindProperty]
    public UsuarioDto UsuarioEditado { get; set; }

    public UsuarioViewModel Usuarios { get; set; }
    public UsuarioDto Usuario { get; set; }

    public async Task OnGet()
    {
      var json = HttpContext.Session.GetString("usuarioLogado");

      if (string.IsNullOrEmpty(json))
      {
        Response.Redirect("/Login");
        return;
      }
      else
      {
        Usuario = JsonConvert.DeserializeObject<UsuarioDto>(json);
      }

      if (!Usuario.isAdmin)
      {
        Response.Redirect("/");
        return;
      }

      var api = new ApiService();

      var resultado = await api.PostAsync<UsuarioViewModel>("User", "GetAllUsers", Usuario);

      Usuarios = resultado.Sucesso ? resultado.Dados : new UsuarioViewModel();
    }

    public async Task<IActionResult> OnPostAtualizar([FromBody] UsuarioItem usuario)
    {
      try
      {
        if (usuario == null)
        {
          return BadRequest(new { success = false, message = "Dados não foram fornecidos." });
        }

        var api = new ApiService();

        var resultado = await api.PostAsync<UsuarioDto>("User", "AdminUpdate", usuario);

        return new JsonResult(new
        {
          success = true,
          message = "Dados recebidos com sucesso!",
          nomeRecebido = usuario.nome,
          departamentoRecebido = usuario.departamento
        });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new
        {
          success = false,
          message = $"Erro interno: {ex.Message}"
        });
      }
    }
  }

  public class UsuarioItem
  {
    public int Id { get; set; }
    public string? nome { get; set; } = string.Empty;
    public string departamento { get; set; } = string.Empty;
    public string? telefone { get; set; } = string.Empty;
    public decimal valor { get; set; }
    public bool pago { get; set; }
    public string forma_pgto { get; set; } = string.Empty;
    public DateTime? data_pagamento { get; set; } = new DateTime?();
    public bool isAdmin { get; set; } = false;
    public bool isScanner { get; set; } = false;
  }
}
