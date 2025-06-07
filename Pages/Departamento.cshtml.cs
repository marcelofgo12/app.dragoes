using DragoesEscola.Models;
using DragoesEscola.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DragoesEscola.Pages
{
  [IgnoreAntiforgeryToken]
  public class DepartamentoModel : PageModel
  {
    [BindProperty]
    public DepartamentoDto depart { get; set; }
    public List<DepartamentoDto> List_Depart { get; set; }
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

      var resultado = await api.PostAsync<List<DepartamentoDto>>("Departamento", "Load", new DepartamentoDto());

      List_Depart = resultado.Sucesso ? resultado.Dados : new List<DepartamentoDto>();
    }

    public async Task<IActionResult> OnPostAtualizar([FromBody] DepartamentoDto departamento)
    {
      try
      {
        if (departamento == null)
        {
          return BadRequest(new { success = false, message = "Dados não foram fornecidos." });
        }

        var api = new ApiService();

        var resultado = await api.PostAsync<UsuarioDto>("Departamento", "InsertOrUpdate", departamento);

        return new JsonResult(new
        {
          success = true,
          message = "Dados recebidos com sucesso!",
          nomeRecebido = departamento.Descricao
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
}
