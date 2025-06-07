using DragoesEscola.Models;
using DragoesEscola.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace DragoesEscola.Pages
{
  [IgnoreAntiforgeryToken]
  public class EventCreateModel : PageModel
  {
    private static UsuarioDto Usuario = new UsuarioDto();
    private static List<EventDto> _events = new List<EventDto>();

    [BindProperty]
    public EventDto NewEvent { get; set; }
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

      if (!Usuario.isAdmin && !Usuario.isScanner)
      {
        Response.Redirect("/");
        return;
      }
    }

    public async Task<JsonResult> OnGetEventsJson()
    {
      var api = new ApiService();

      var resultado = await api.GetAsync<List<EventDto>>("Event", "Load");

      _events = resultado.Sucesso ? resultado.Dados : new List<EventDto>();

      return new JsonResult(_events);
    }

    public async Task<JsonResult> OnPostCreateEventJson([FromBody] EventDto newEvent)
    {
      if (newEvent == null)
      {
        return new JsonResult(new { error = "Dados do evento inválidos." }) { StatusCode = 400 };
      }

      var api = new ApiService();

      var resultado = await api.PostAsync<UsuarioDto>("Event", "InsertOrUpdate", newEvent);

      return new JsonResult(new
      {
        success = true,
        message = "Evento criado com sucesso!",
        nomeRecebido = newEvent.Descricao
      });
    }

    public async Task<JsonResult> OnPutUpdateEventJson([FromRoute] int id, [FromBody] EventDto updatedEvent)
    {
      if (updatedEvent == null || id != updatedEvent.id)
      {
        return new JsonResult(new { error = "Dados do evento inválidos ou ID não corresponde." }) { StatusCode = 400 };
      }

      var existingEvent = _events.FirstOrDefault(e => e.id == id);
      if (existingEvent == null)
      {
        return new JsonResult(new { error = "Evento não encontrado." }) { StatusCode = 404 };
      }

      var api = new ApiService();

      var resultado = await api.PostAsync<UsuarioDto>("Event", "InsertOrUpdate", updatedEvent);

      return new JsonResult(new
      {
        success = true,
        message = "Evento criado com sucesso!",
        nomeRecebido = updatedEvent.Descricao
      });
    }

  }
}
