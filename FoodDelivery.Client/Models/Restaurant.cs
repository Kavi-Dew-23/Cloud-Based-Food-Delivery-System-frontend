public class Restaurant
{
    public int Id { get; set; }
    public string Name {  get; set; }
    public string ImageUrl {  get; set; }
    public string Address { get; set; }
    public List<MenuCategory> MenuCategories { get; set; } = new List<MenuCategory>();

}

public class MenuCategory
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}

public class MenuItem
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
}

