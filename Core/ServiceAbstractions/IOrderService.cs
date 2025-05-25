using Shared.DTO.OrderModule;

namespace ServiceAbstractions
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(string email, CreateOrderDto createOrderDto);
        Task <DeliveryMethodDto> GetDeliveryMethods();
        Task<IEnumerable<OrderDto>> GetAllOrdersForCurrentUser(string email);
        Task<OrderDto> GetOrderByIdForCurrentUser(Guid id);
    }
}
