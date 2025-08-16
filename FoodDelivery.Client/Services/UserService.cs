using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FoodDelivery.Client.Services
{
    public class UserService
    {
        public readonly HttpClient _http;

        public UserService(HttpClient http)
        {

            _http = http;
        }
        public async Task<string> GetStatusAsync()
        {
            try
            {
                // Use absolute path to avoid routing conflicts
                var response = await _http.GetAsync("http://localhost/user/api/status");
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return $"Connection failed: {ex.Message}";
            }
        }
    }
}
