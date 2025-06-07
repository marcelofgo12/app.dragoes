using DragoesEscola.Models;
using DragoesEscola.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Newtonsoft.Json;

namespace DragoesEscola.Pages
{
  public class RegisterModel : PageModel
  {
    [BindProperty]
    public UsuarioDto Input { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
      bool continuePersist = true;
      if (string.IsNullOrEmpty(Input.nome))
      {
        ModelState.AddModelError(string.Empty, "Nome é obrigatório");
        continuePersist = false;
      }
      if (string.IsNullOrEmpty(Input.rg))
      {
        ModelState.AddModelError(string.Empty, "O RG é obrigatório");
        continuePersist = false;
      }
      if (!IsDataValida(Input.data_nascimento + ""))
      {
        ModelState.AddModelError(string.Empty, "A data de nascimento inválida");
        continuePersist = false;
      }
      if (string.IsNullOrEmpty(Input.telefone))
      {
        ModelState.AddModelError(string.Empty, "O telefone é obrigatório");
        continuePersist = false;
      }
      if (string.IsNullOrEmpty(Input.foto))
      {
        ModelState.AddModelError(string.Empty, "A foto é obrigatória");
        continuePersist = false;
      }
      if (string.IsNullOrEmpty(Input.LoginPass))
      {
        ModelState.AddModelError(string.Empty, "Informe uma senha");
        continuePersist = false;
      }

      if (!continuePersist)
        return Page();

      var api = new ApiService();

      var request = HttpContext.Request;
      var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";

      Input.baseUrl = baseUrl;

      var resultado = await api.PostAsync<UsuarioDto>("User", "Cadastro", Input);

      if (resultado.Sucesso)
      {
        TempData["CadastroStatus"] = "sucesso";
        TempData["Mensagem"] = "Cadastro realizado com sucesso!";
      }
      else
      {
        ModelState.AddModelError(string.Empty,
             resultado.Mensagem != null
                 ? JsonConvert.DeserializeObject<dynamic>(resultado.Mensagem)?.mensagem?.ToString()
                 : "Erro ao realizar cadastro.");

        return Page();
      }

      return RedirectToPage();
    }

    public static bool IsDataValida(string dataTexto)
    {
      DateTime dataConvertida;
      return DateTime.TryParseExact(
          dataTexto,
          "dd/MM/yyyy HH:mm:ss",
          System.Globalization.CultureInfo.InvariantCulture,
          System.Globalization.DateTimeStyles.None,
          out dataConvertida
      );
    }
  }
}
