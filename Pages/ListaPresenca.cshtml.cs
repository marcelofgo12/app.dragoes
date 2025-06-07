using DragoesEscola.Models;
using DragoesEscola.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragoesEscola.Pages
{
  public class ScannedQRCode
  {
    public DateTime? Data { get; set; }
    public string UserNome { get; set; } = string.Empty;
    public string Carterinha { get; set; } = string.Empty;
    public string Departamento { get; set; } = string.Empty;
  }

  public class ListaPresencaModel : PageModel
  {
    private static List<ScannedQRCode> _scannedQRCodes = new List<ScannedQRCode>();

    [BindProperty(SupportsGet = true)] 
    public string QrCodeData { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string EventId { get; set; } = string.Empty;
    public InviteDto Input { get; set; } = new InviteDto();

    public List<ScannedQRCode> ScannedQRCodes { get; set; } = new List<ScannedQRCode>();

    public async Task OnGet(string eventId)
    {
      _scannedQRCodes = new List<ScannedQRCode>();
      Input.EventId = eventId;

      var api = new ApiService();

      var resultado = await api.PostAsync<List<InviteModel>>("Invite", "GetAllByEvent", eventId);

      if (resultado.Sucesso)
      {
        foreach(var item in resultado.Dados)
        {
          var model = new ScannedQRCode()
          {
            Data = item.HoraChegada,
            UserNome = item.User.nome,
            Departamento = item.User.departamento,
            Carterinha = (item.User.n_carteirinha + "").PadLeft(6, '0')
          };

          _scannedQRCodes.Add(model);
        }
      }

      ScannedQRCodes = _scannedQRCodes.OrderBy(q => q.Data).ToList();
    }

    public async Task<IActionResult> OnPostScan()
    {
      if (!string.IsNullOrEmpty(QrCodeData))
      {
        var existingCode = _scannedQRCodes.FirstOrDefault(q => q.Carterinha == QrCodeData);

        if (existingCode == null)
        {

          Input.UserId = QrCodeData; 
          Input.EventId = EventId; 

          var api = new ApiService();

          var resultado = await api.PostAsync<InviteModel>("Invite", "Insert", Input);

          if (resultado.Sucesso) {
            var newScannedCode = new ScannedQRCode
            {
              Carterinha = QrCodeData,
              Data = Convert.ToDateTime(resultado.Dados.HoraChegada),
              Departamento = resultado.Dados.User.departamento + "",
              UserNome = resultado.Dados.User.nome + ""
            };
            _scannedQRCodes.Add(newScannedCode);

            return new JsonResult(new
            {
              success = true,
              message = "QR Code lido com sucesso!",
              qrCode = newScannedCode
            });
          }
          else
          {
            return new JsonResult(new
            {
              success = false,
              message = "Erro ao ler QR Code."
            });
          }
        }
        else
        {
          return new JsonResult(new
          {
            success = false,
            message = "QR Code já lido."
          });
        }
      }

      return new JsonResult(new
      {
        success = false,
        message = "Nenhum dado de QR Code recebido."
      });
    }
  }
}