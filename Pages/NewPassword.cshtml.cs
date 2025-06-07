using DragoesEscola.Models;
using DragoesEscola.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace DragoesEscola.Pages
{
  public class NewPasswordModel : PageModel
  {
    [BindProperty]
    public ResetPasswordViewModel Input { get; set; } = new ResetPasswordViewModel();

    public void OnGet(string token)
    {
      Input.tokenPassword = token;
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
        return Page();

      var api = new ApiService();

      var resultado = await api.PostAsync<UsuarioDto>("User", "NewPassword", Input);

      if (resultado.Sucesso)
      {
        TempData["ForgotStatus"] = "sucesso";
        TempData["Mensagem"] = "Senha alterada com sucesso!";
      }
      else
      {
        string _mensagem = resultado.Mensagem != null
                 ? JsonConvert.DeserializeObject<dynamic>(resultado.Mensagem)?.mensagem?.ToString()
                 : "Erro ao alterar senha.";

        TempData["ForgotStatus"] = "ERROR";
        TempData["Mensagem"] = _mensagem;

        return Page();
      }

      return RedirectToPage();
    }
  }

  public class ResetPasswordViewModel
  {
    public string tokenPassword { get; set; }
    public string LoginPass { get; set; } = string.Empty;
    public string ConfirmLoginPass { get; set; } = string.Empty;
  }
}
