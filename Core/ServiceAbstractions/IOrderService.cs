using Shared.DTO.OrderModule;

namespace ServiceAbstractions
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(string email,CreateOrderDto createOrderDto);

    }
}
