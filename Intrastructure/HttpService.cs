using CSharpFunctionalExtensions;
using NYC311Dashboard.Intrastructure.Contracts;
using System.Net.Http.Json;
using System.Text.Json;

namespace NYC311Dashboard.Intrastructure
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<T>> GetAsync<T>(string url) where T : class
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                };

                var response = await _httpClient.GetFromJsonAsync<T>(url, options);

                return response;
            }
            catch (Exception ex)
            {
                return Result.Failure<T>(ex.Message);
            }

            //var responseContent = await response.Content.ReadAsStringAsync();
        }

        // todo: Add other methods (PostAsync, PutAsync)?
    }
}
