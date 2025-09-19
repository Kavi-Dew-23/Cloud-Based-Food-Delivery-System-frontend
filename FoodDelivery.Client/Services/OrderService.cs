using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;

namespace FoodDelivery.Client.Services
{
    public class OrderService
    {
        public event Action OnChange;
        private readonly List<MenuItemSize> cartItems = new();

        public void AddToCart(MenuItemSize item)
        {
            cartItems.Add(item);
            NotifyStateChanged();
        }

        //remove items from cart
        public void RemoveFromCart(int SizeId, string SizeName)
        {
            var item = cartItems.FirstOrDefault(r => r.SizeId == SizeId && r.SizeName == SizeName);
            if(item != null)
            {
                cartItems.Remove(item);
                NotifyStateChanged();
            }
            
        }

        public int GetCartItemsCount()
        {
            return cartItems.Count;
        }

        public List<MenuItemSize> GetCartItems()
        {
            return cartItems;
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
 }
}


/*
public readonly HttpClient _http;

public OrderService(HttpClient http)
{

    _http = http;
}


 * check the status of the order service
public async Task<string> GetStatusAsync()
{
    try
    {
        // Use absolute path to avoid routing conflicts
        var response = await _http.GetAsync("http://localhost/order/api/status");
        return await response.Content.ReadAsStringAsync();
    }
    catch (Exception ex)
    {
        return $"Connection failed: {ex.Message}";
    }
}
*/

