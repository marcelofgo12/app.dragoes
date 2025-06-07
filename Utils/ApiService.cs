using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DragoesEscola.Utils
{
  public class ApiService
  {
    private readonly HttpClient _httpClient;
    //private const string BaseUrl = "https://localhost:7147/api";
    private const string BaseUrl = "https://api.dragoescarnaval.com.br/api";

    public ApiService()
    {
      _httpClient = new HttpClient();
    }

    private string BuildUrl(string controller, string metodo)
    {
      return $"{BaseUrl}/{controller}/{metodo}";
    }

    public async Task<ApiResponse<T>> PostAsync<T>(string controller, string metodo, object data, string token = null)
    {
      var url = BuildUrl(controller, metodo);
      var json = JsonConvert.SerializeObject(data);
      var content = new StringContent(json, Encoding.UTF8, "application/json");

      if (!string.IsNullOrEmpty(token))
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

      var response = await _httpClient.PostAsync(url, content);

      var body = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode)
      {
        var result = JsonConvert.DeserializeObject<T>(body);
        return ApiResponse<T>.Success(result);
      }
      else
      {
        return ApiResponse<T>.Failure(body);
      }
    }

    public async Task<ApiResponse<T>> GetAsync<T>(string controller, string metodo, string token = null)
    {
      var url = BuildUrl(controller, metodo);

      if (!string.IsNullOrEmpty(token))
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

      var response = await _httpClient.GetAsync(url);
      var body = await response.Content.ReadAsStringAsync();

      if (response.IsSuccessStatusCode)
      {
        var result = JsonConvert.DeserializeObject<T>(body);
        return ApiResponse<T>.Success(result);
      }
      else
      {
        return ApiResponse<T>.Failure(body);
      }
    }
  }

  public class ApiResponse<T>
  {
    public bool Sucesso { get; private set; }
    public string Mensagem { get; private set; }
    public T Dados { get; private set; }

    public static ApiResponse<T> Success(T data) => new ApiResponse<T>
    {
      Sucesso = true,
      Dados = data,
      Mensagem = null
    };

    public static ApiResponse<T> Failure(string message) => new ApiResponse<T>
    {
      Sucesso = false,
      Mensagem = message,
      Dados = default
    };
  }
}
