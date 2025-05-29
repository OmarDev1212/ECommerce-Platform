using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderAggregate;
using DomainLayer.Models.ProductModule;
using Service.Specifications;
using ServiceAbstractions;
using Shared.DTO.IdentityModule;
using Shared.DTO.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IMapper mapper, IUnitOfWork unitOfWork, IBasketRepository basketRepository) : IOrderService
    {
        public async Task<OrderDto> CreateOrderAsync(string email, CreateOrderDto createOrderDto)
        {
            //1. get  address
            var mappedAddress = mapper.Map<AddressDto, ShippingAddress>(createOrderDto.shipToAddress);
            //2. get delivery method
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetById(createOrderDto.DeliveryMethodId) ?? throw new DeliveryMethodNotFoundException(createOrderDto.DeliveryMethodId);
            //3. get basket items

            var basket = await basketRepository.GetCustomerBasket(createOrderDto.BasketId) ?? throw new BasketNotFoundException(createOrderDto.BasketId);

            ArgumentNullException.ThrowIfNull(basket.paymentIntentId);

            var spec = new OrderByPaymentIntentId(basket.paymentIntentId);


            var orderRepo = unitOfWork.GetRepository<Order, Guid>();

            var ExisitingOrder = await orderRepo.GetById(spec);
            if(ExisitingOrder is not null)
                orderRepo.Delete(ExisitingOrder);
            List<OrderItem> orderItems = [];
            var productRepo = unitOfWork.GetRepository<Product, int>();
            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetById(item.Id) ?? throw new ProductNotFoundException(item.Id);
                var orderItem = new OrderItem()
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrdered { PictureUrl = product.PictureUrl, ProductId = product.Id.ToString(), ProductName = product.Name }
                };
                orderItems.Add(orderItem);
            }
            //4.subtotal
            var subTotal = orderItems.Sum(i => i.Price * i.Quantity);

            //5.create order
            var order = new Order(email, mappedAddress, deliveryMethod, createOrderDto.DeliveryMethodId, orderItems, subTotal, basket.paymentIntentId);
            //6. save to database
            await orderRepo.AddAsync(order);

            var result = await unitOfWork.SaveChangesAsync();
            if (result <= 0)
                throw new BadRequestException("Bad Data passed To Order");

            //return new OrderDto()
            //{
            //    Address = createOrderDto.ShippingAddress,
            //    DeliveryMethod = deliveryMethod.ShortName,
            //    Items = mapper.Map<IEnumerable<OrderItemDto>>(orderItems),
            //    OrderDate = order.OrderDate,
            //    Status = order.Status.ToString(),
            //    SubTotal = subTotal,
            //    TotalPrice = order.GetTotal(),
            //    UserEmail = order.UserEmail,
            //    Id=order.Id.ToString()
            //};
            return mapper.Map<OrderDto>(order);
        }


        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethods()
        {
            var methods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAll();
            return mapper.Map<IEnumerable<DeliveryMethodDto>>(methods);
        }
        public async Task<IEnumerable<OrderDto>> GetAllOrdersForCurrentUser(string email)
        {
            var spec = new OrdersByEmailSpecifcation(email);
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAll(spec);
            return mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderByIdForCurrentUser(Guid id)
        {
            var spec = new OrderByIdSpecification(id);
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetById(spec);
            return mapper.Map<OrderDto>(orders);
        }
    }
}
