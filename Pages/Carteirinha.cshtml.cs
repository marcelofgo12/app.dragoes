using DragoesEscola.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace DragoesEscola.Pages
{
  public class CarteirinhaModel : PageModel
  {
    public UsuarioDto Usuario { get; set; }

    public void OnGet()
    {
      var json = HttpContext.Session.GetString("usuarioLogado");

      if (string.IsNullOrEmpty(json))
      {
        // Redireciona se não estiver logado
        Response.Redirect("/Login");
        return;
      }

      Usuario = JsonSerializer.Deserialize<UsuarioDto>(json);
    }
  }
}
