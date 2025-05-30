namespace Shared.DTO.BasketModule
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public ICollection<BasketItemDto> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
