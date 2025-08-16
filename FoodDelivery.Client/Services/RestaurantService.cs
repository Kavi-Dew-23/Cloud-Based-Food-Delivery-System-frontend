using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FoodDelivery.Client.Services
{
    public class RestaurantService
    {
        public readonly HttpClient _http;

        public RestaurantService(HttpClient http)
        {

            _http = http;
        }
        public async Task<string> GetStatusAsync()
        {
            try
            {
                // Use absolute path to avoid routing conflicts
                var response = await _http.GetAsync("http://localhost/restaurant/api/status");
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return $"Connection failed: {ex.Message}";
            }
        }
    }
}
