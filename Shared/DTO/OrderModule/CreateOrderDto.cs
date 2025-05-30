using Shared.DTO.IdentityModule;

namespace Shared.DTO.OrderModule
{
    public class CreateOrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto shipToAddress { get; set; }
    }
}
