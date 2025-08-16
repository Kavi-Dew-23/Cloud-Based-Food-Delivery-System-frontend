namespace FoodDelivery.Client.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public List<OrderItem> Items { get; set; }
    }
    public class OrderItem
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
