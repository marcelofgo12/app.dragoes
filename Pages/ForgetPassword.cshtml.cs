using DragoesEscola.Models;
using DragoesEscola.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DragoesEscola.Pages
{
  public class ForgetPasswordModel : PageModel
  {
    [BindProperty]
    public ForgotPasswordViewModel Input { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
        return Page();

      var api = new ApiService();

      var resultado = await api.PostAsync<UsuarioDto>("User", "ForgotPassword", Input);

      if (resultado.Sucesso)
      {
        TempData["ForgotStatus"] = "sucesso";
        TempData["Mensagem"] = "Enviado link para recuperação de senha ao telefone informado!";
      }
      else
      {
        ModelState.AddModelError(string.Empty,
             resultado.Mensagem != null
                 ? JsonConvert.DeserializeObject<dynamic>(resultado.Mensagem)?.mensagem?.ToString()
                 : "Erro ao enviar mensagem.");

        return Page();
      }

      return RedirectToPage();
    }
  }
  public class ForgotPasswordViewModel
  {
    public string telefone { get; set; }
    public string? baseUrl { get; set; } = string.Empty;
  }
}
