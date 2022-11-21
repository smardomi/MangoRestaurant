using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class BaseService : IBaseService
    {
        public IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> SendAsync<T>(ApiRequest request) 
        {
            var client = _httpClientFactory.CreateClient("Mango");
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(request.Url);
            if (request.Data != null)
            {
                message.Content = new StringContent(JsonSerializer.Serialize(request.Data), Encoding.UTF8, "application/json");
            }

            if (!string.IsNullOrWhiteSpace(request.AccessToken))
            {
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", request.AccessToken);
            }

            HttpResponseMessage? response = null;
            message.Method = request.ApiType switch
            {
                ApiUtil.ApiType.GET => HttpMethod.Get,
                ApiUtil.ApiType.POST => HttpMethod.Post,
                ApiUtil.ApiType.PUT => HttpMethod.Put,
                ApiUtil.ApiType.DELETE => HttpMethod.Delete,
                _ => throw new ArgumentOutOfRangeException()
            };

            response = await client.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var apiContent = await response.Content.ReadAsStringAsync();
                var apiResultDto = JsonSerializer.Deserialize<ApiResult<T>>(apiContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (apiResultDto.IsSuccess)
                {
                    return apiResultDto.Data;
                }
                throw new Exception(apiResultDto.Message);
            }
            
            throw new Exception(response.ReasonPhrase);

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
