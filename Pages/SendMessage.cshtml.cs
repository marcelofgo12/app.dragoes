using DragoesEscola.Models;
using DragoesEscola.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DragoesEscola.Pages
{
  public class SendMessageModel : PageModel
  {
    [BindProperty]
    public MessageInputModel Message { get; set; } = new();
    public UsuarioDto Usuario { get; set; }

    [TempData]
    public string? ResultMessage { get; set; }

    public void OnGet()
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
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
        return Page();

      var api = new ApiService();
      var resultado = await api.PostAsync<bool>("User", "SendMessage", Message);

      if (resultado.Sucesso)
      {
        return RedirectToPage();
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Erro ao enviar mensagem.");
        return Page();
      }
    }

    public class MessageInputModel
    {
      public int To { get; set; } = 1;
      public string Subject { get; set; } = string.Empty;
      public string Body { get; set; } = string.Empty;
    }
  }
}