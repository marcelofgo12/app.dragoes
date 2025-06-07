using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using DragoesEscola.Models;
using DragoesEscola.Utils;
using Microsoft.AspNetCore.Http;

namespace DragoesEscola.Pages
{
  public class IndexModel : PageModel
  {
    [BindProperty]
    public LoginInputModel Input { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
        return Page();


      var api = new ApiService();

      var resultado = await api.PostAsync<UsuarioDto>("User", "Login", Input);

      if (resultado.Sucesso)
      {
        var usuario = resultado.Dados;
        var usuarioJson = JsonSerializer.Serialize(usuario);
        HttpContext.Session.SetString("usuarioLogado", usuarioJson);
        HttpContext.Session.SetString("FotoUsuario", usuario.foto);
        HttpContext.Session.SetString("UserAdmin", usuario.isAdmin.ToString());
        HttpContext.Session.SetString("UserScanner", usuario.isScanner.ToString());

        return RedirectToPage("/Home");
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Login inválido.");
        return Page();
      }
    }
  }
  public class LoginInputModel
  {
    public string telefone { get; set; }
    public string LoginPass { get; set; }
  }
}
