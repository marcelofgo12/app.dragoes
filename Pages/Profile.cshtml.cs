using DragoesEscola.Models;
using DragoesEscola.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DragoesEscola.Pages
{
  public class ProfileModel : PageModel
  {
    [BindProperty]
    public UsuarioDto Usuario { get; set; } = new UsuarioDto();
    public void OnGet()
    {
      var json = HttpContext.Session.GetString("usuarioLogado");

      if (string.IsNullOrEmpty(json))
      {
        // Redireciona se não estiver logado
        Response.Redirect("/Login");
        return;
      }

      Usuario = JsonConvert.DeserializeObject<UsuarioDto>(json);
    }

    public async Task<IActionResult> OnPostAsync()
    {
      if (!ModelState.IsValid)
        return Page();

      var api = new ApiService();

      var resultado = await api.PostAsync<UsuarioDto>("User", "UpdateCadastro", Usuario);
     
      if (resultado.Sucesso)
      {
        TempData["Sucess"] = true;
        TempData["CadastroStatus"] = "sucess";
        TempData["Mensagem"] = "Cadastro alterado com sucesso! ";

        var usuarioJson = JsonConvert.SerializeObject(resultado.Dados);
        HttpContext.Session.SetString("usuarioLogado", usuarioJson);
        HttpContext.Session.SetString("FotoUsuario", Usuario.foto);

      }
      else
      {
        ModelState.AddModelError(string.Empty,
             resultado.Mensagem != null
                 ? JsonConvert.DeserializeObject<dynamic>(resultado.Mensagem)?.mensagem?.ToString()
                 : "Erro ao alterar cadastro.");

        return Page();
      }

      return RedirectToPage();
    }

  }
}
