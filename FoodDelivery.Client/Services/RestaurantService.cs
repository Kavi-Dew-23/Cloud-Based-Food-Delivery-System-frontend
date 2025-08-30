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
                    Name = "Pizza Hut",
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
                                    ImageUrl="/images/pizzahut/Middle-eastern-chicken-kofta.png",
                                    MenuItemSizes = new List<MenuItemSize>
                                    {
                                        new MenuItemSize
                                        {
                                            SizeId = 1,
                                            SizeName = "Personal",
                                            SizePrice = 1100.00
                                        },
                                        new MenuItemSize
                                        {
                                            SizeId = 2,
                                            SizeName = "Medium",
                                            SizePrice = 2000.00
                                        },
                                        new MenuItemSize
                                        {
                                            SizeId = 3,
                                            SizeName = "Large",
                                            SizePrice = 3780.00
                                        }
                                    }

                                },
                                new MenuItem
                                {
                                    ItemId = 2,
                                    Name = "Teriyaki Chicken Pizza",
                                    Description = "Savour a taste of Japan with our Teriyaki Chicken Pizza – made with teriyaki chicken and mozzarella, topped with a luscious drizzle of authentic Teriyaki sauce.",
                                    Price = 1240.00,
                                    ImageUrl = "/images/pizzahut/Teriyaki-Chicken.png",
                                    MenuItemSizes = new List<MenuItemSize>
                                    {
                                        new MenuItemSize
                                        {
                                            SizeId = 1,
                                            SizeName = "Personal",
                                            SizePrice = 1240.00
                                        },
                                        new MenuItemSize
                                        {
                                            SizeId = 2,
                                            SizeName = "Medium",
                                            SizePrice = 2220.00
                                        },
                                        new MenuItemSize
                                        {
                                            SizeId = 3,
                                            SizeName = "Large",
                                            SizePrice = 4200.00
                                        }
                                    }
                                },
                                new MenuItem
                                {
                                    ItemId = 3,
                                    Name = "Crispy Korean Chicken",
                                    Description = "Taste the crunch with our Crispy Korean Chicken Pizza - popcorn chicken on a spicy Korean sauce base, topped with mozzarella and an extra kick of our bold Korean sauce.",
                                    Price = 1240.00,
                                    ImageUrl = "/images/pizzahut/Crispy-Korean-chicken.png",
                                    MenuItemSizes = new List<MenuItemSize>
                                    {
                                        new MenuItemSize
                                        {
                                            SizeId = 1,
                                            SizeName = "Personal",
                                            SizePrice = 1240.00
                                        },
                                        new MenuItemSize
                                        {
                                            SizeId = 2,
                                            SizeName = "Medium",
                                            SizePrice = 2220.00
                                        },
                                        new MenuItemSize
                                        {
                                            SizeId = 3,
                                            SizeName = "Large",
                                            SizePrice = 4200.00
                                        }
                                    }
                                },
                                new MenuItem
                                {
                                    ItemId = 4,
                                    Name = "Thai Green Curry Chicken",
                                    Description = "Turn up the heat with our Thai Green Curry Chicken Pizza - a zesty green curry sauce base topped with tender green curry chicken, onions and mozzarella.",
                                    Price = 1240.00,
                                    ImageUrl = "/images/pizzahut/Thai-Green-Curry-Chicken.png",
                                    MenuItemSizes = new List<MenuItemSize>
                                    {
                                        new MenuItemSize
                                        {
                                            SizeId = 1,
                                            SizeName = "Personal",
                                            SizePrice = 1240.00
                                        },
                                        new MenuItemSize
                                        {
                                            SizeId = 2,
                                            SizeName = "Medium",
                                            SizePrice = 2220.00
                                        },
                                        new MenuItemSize
                                        {
                                            SizeId = 3,
                                            SizeName = "Large",
                                            SizePrice = 4200.00
                                        }
                                    }
                                },
                                new MenuItem
                                {
                                    ItemId = 5,
                                    Name = "Middle Eastern Chicken Kofta - Thin Crust",
                                    Price = 1100.00,
                                    ImageUrl="/images/pizzahut/Middle-eastern-chicken-kofta-ThinCrust.png",
                                    MenuItemSizes = new List<MenuItemSize>
                                    {
                                        new MenuItemSize
                                        {
                                            SizeId = 1,
                                            SizeName = "Regular",
                                            SizePrice = 1650.00
                                        },
                                        new MenuItemSize
                                        {
                                            SizeId = 2,
                                            SizeName = "Large",
                                            SizePrice = 3080.00
                                        },
                                        
                                    }

                                },
                                new MenuItem
                                {
                                    ItemId = 6,
                                    Name = "Teriyaki Chicken Pizza - Thin Crust",
                                    Price = 1760.00,
                                    ImageUrl = "/images/pizzahut/Teriyaki-Chicken-ThinCrust.png",
                                    MenuItemSizes = new List<MenuItemSize>
                                    {
                                        new MenuItemSize
                                        {
                                            SizeId = 1,
                                            SizeName = "Regular",
                                            SizePrice = 1760.00,
                                        },
                                        new MenuItemSize
                                        {
                                            SizeId=2,
                                            SizeName="Large",
                                            SizePrice = 3410.00,
                                        }
                                    }
                                },
                                new MenuItem
                                {
                                    ItemId = 7,
                                    Name = "Crispy Korean Chicken - Thin Crust",
                                    Price = 1760.00,
                                    ImageUrl = "/images/pizzahut/Crispy-Korean-chicken-ThinCrust.png",
                                    MenuItemSizes = new List<MenuItemSize>
                                    {
                                        new MenuItemSize
                                        {
                                            SizeId = 1,
                                            SizeName = "Regular",
                                            SizePrice = 1760.00,
                                        },
                                        new MenuItemSize
                                        {
                                            SizeId=2,
                                            SizeName="Large",
                                            SizePrice = 3410.00,
                                        }
                                    }
                                },
                                new MenuItem
                                {
                                    ItemId = 8,
                                    Name = "Thai Green Curry Chicken - Thin Crust",
                                    Description = "Turn up the heat with our Thai Green Curry Chicken Pizza - a zesty green curry sauce base topped with tender green curry chicken, onions and mozzarella.",
                                    Price = 1240.00,
                                    ImageUrl = "/images/pizzahut/Thai-Green-Curry-Chicken-ThinCrust.png",
                                    MenuItemSizes = new List<MenuItemSize>
                                    {
                                        new MenuItemSize
                                        {
                                            SizeId = 1,
                                            SizeName = "Regular",
                                            SizePrice = 1760.00,
                                        },
                                        new MenuItemSize
                                        {
                                            SizeId=2,
                                            SizeName="Large",
                                            SizePrice = 3410.00,
                                        }
                                    }
                                },
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
                                    Price = 1060.00,
                                    ImageUrl= "/images/pizzahut/Double-Chicken-Delight.jpg"
                                },
                                new MenuItem
                                {
                                    ItemId= 2,
                                    Name = "Spicy Chicken Combo Melts",
                                    Description = "A Crunchy folded dough Loaded with Spicy Chicken, Kotchchi Meat, Onion, and Cheese baked to golden perfection",
                                    Price = 1060.00,
                                    ImageUrl= "/images/pizzahut/Spicy-Chicken-Combo.jpg"
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
