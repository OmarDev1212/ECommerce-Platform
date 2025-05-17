namespace DomainLayer.Models.BasketModule
{
    public class BasketItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
