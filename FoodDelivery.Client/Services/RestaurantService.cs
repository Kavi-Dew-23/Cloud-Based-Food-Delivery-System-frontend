using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FoodDelivery.Client.Services
{
    public interface IRestaurantService
    {
        Task<Restaurant> GetRestaurantByIdAsync(int id);
        Task<List<Restaurant>> GetRestaurantsAsync();
        Task<string> GetStatusAsync();
    }
    public class RestaurantService : IRestaurantService
    {
        public readonly HttpClient _http;
        private readonly List<Restaurant> _fallbackRestuarants;

        public RestaurantService(HttpClient http)
        {

            _http = http;
            _fallbackRestuarants = CreateFallBackData();
        }

        // check the status of the restuarant
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

        // Get the restaurants by its Id
        public async Task<Restaurant> GetRestaurantByIdAsync(int id)
        {
            try
            {
                // try to get from API first
                return await _http.GetFromJsonAsync<Restaurant>($"/api/restuarants/{id}");
            }
            catch (Exception ex)
            {
                //fallback to local data if API is unavailable
                return _fallbackRestuarants.FirstOrDefault(r =>  r.Id == id);
            }
        }

        //Get all restuarants
        public async Task<List<Restaurant>> GetRestaurantsAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<Restaurant>>("/api/restaurants");
            }
            catch (Exception ex)
            {
                // fallback to local data
                return _fallbackRestuarants;
            }
        }

        private List<Restaurant> CreateFallBackData()
        {
            return new List<Restaurant>
            {
                new Restaurant
                {
                    Id = 1,
                    Name = "Pizza hut",
                    MenuCategories = new List<MenuCategory>
                    {
                        new MenuCategory
                        {
                            CategoryId = 1,
                            Name = "New Arrivals",
                            MenuItems = new List<MenuItem>
                            {
                                new MenuItem
                                {
                                    ItemId = 1,
                                    Name = "Middle Eastern Chicken Kofta",
                                    Description = "This Pizza is a fusion of Middle Eastern flavours with juicy chicken kofta, cream cheese, fresh coriander, onions and mozzarella, all drizzled with our signature Arabic sauce",
                                    Price = 1100.00,
                                },
                                new MenuItem
                                {
                                    ItemId = 2,
                                    Name = "Teriyaki Chicken Pizza",
                                    Description = "Savour a taste of Japan with our Teriyaki Chicken Pizza – made with teriyaki chicken and mozzarella, topped with a luscious drizzle of authentic Teriyaki sauce.",
                                    Price = 1240.00
                                },
                                new MenuItem
                                {
                                    ItemId = 3,
                                    Name = "Crispy Korean Chicken",
                                    Description = "Taste the crunch with our Crispy Korean Chicken Pizza - popcorn chicken on a spicy Korean sauce base, topped with mozzarella and an extra kick of our bold Korean sauce.",
                                    Price = 1240.00
                                }
                            }
                        },
                        new MenuCategory
                        {
                            CategoryId = 2,
                            Name = "Melts",
                            MenuItems = new List<MenuItem>
                            {
                                new MenuItem
                                {
                                    ItemId = 1,
                                    Name = "Double Chicken Delight Melts",
                                    Description = "A Crunchy folded dough Loaded with Chicken Bacon, Roast Chicken, Onion, and Cheese baked to golden perfection",
                                    Price = 1060.00
                                },
                                new MenuItem
                                {
                                    ItemId= 2,
                                    Name = "Spicy Chicken Combo Melts",
                                    Description = "A Crunchy folded dough Loaded with Spicy Chicken, Kotchchi Meat, Onion, and Cheese baked to golden perfection",
                                    Price = 1060.00
                                }
                            }

                        }
                    }
                },
                new Restaurant
                {
                    Id = 5,
                    Name = "Madeena Beach Hotel",
                    MenuCategories = new List<MenuCategory>
                    {
                        new MenuCategory
                        {
                            CategoryId = 1,
                            Name = "Cheese Kottu",
                            MenuItems = new List<MenuItem>
                            {
                                new MenuItem
                                {
                                    ItemId = 1,
                                    Name = "Chicken Cheese Kottu",
                                    Price = 2070.00
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
