using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderAggregate;
using Microsoft.Extensions.Configuration;
using Service.Specifications;
using ServiceAbstractions;
using Shared.DTO.BasketModule;
using Stripe;

namespace Service
{
    public class PaymentService(IConfiguration configuration, IBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper mapper) : IPaymentService
    {
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(string basketId)
        {
            // configure stripe : Install package Stripe.Net
            StripeConfiguration.ApiKey = configuration.GetSection("StripeSettings")["SecretKey"];

            //Get Basket By basketId
            var basket = await basketRepository.GetCustomerBasket(basketId) ?? throw new BasketNotFoundException(basketId);


            // get amount -get  product + delivery method cost
            var productRepo = unitOfWork.GetRepository<DomainLayer.Models.ProductModule.Product, int>();

            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetById(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
                item.Name = product.Name;
                item.PictureUrl = product.PictureUrl;
            }
            var deliveryMethodRepo = unitOfWork.GetRepository<DeliveryMethod, int>();
            var deliveryMethod = await deliveryMethodRepo.GetById(basket.deliveryMethodId.Value) ?? throw new DeliveryMethodNotFoundException(basket.deliveryMethodId.Value);
            
            basket.shippingPrice = deliveryMethod.Price;

            var subTotal = basket.Items.Sum(i => i.Quantity * i.Price);
            var amount = (long)(subTotal + deliveryMethod.Price) * 100;


            //create or update paymentIntent
            var service = new PaymentIntentService();
            if (basket.paymentIntentId is null)//create
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };

                PaymentIntent paymentIntent = service.Create(options);
                basket.paymentIntentId = paymentIntent.Id;
                basket.clientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };

                await service.UpdateAsync(basket.paymentIntentId, options);
            }
            var basketToReturn = await basketRepository.CreateOrUpdateBasket(basket);
            return mapper.Map<CustomerBasketDto>(basketToReturn);
        }

        public async Task UpdateOrderStatus(string jsonRequest, string stripeHeader)
        {
            var whSecret = configuration["StripeSettings:WebHookSecret"];


            var stripeEvent = EventUtility.ConstructEvent(jsonRequest, stripeHeader,
                    whSecret);
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentSucceeded:
                    // Then define and call a method to handle the successful payment intent.
                    await UpdatePaymentIntentSucceded(paymentIntent.Id);
                    break;
                case EventTypes.PaymentIntentPaymentFailed:
                    {
                        var paymentMethod = stripeEvent.Data.Object as PaymentMethod;

                        await UpdatePaymentIntentFailed(paymentIntent.Id);
                        break;
                    }

                // ... handle other event types
                default:
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }
        }

        private async Task UpdatePaymentIntentSucceded(string id)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetById(new OrderByPaymentIntentId(id));
            order.Status = OrderStatus.PaymentRecieved;
            unitOfWork.GetRepository<Order,Guid>().Update(order);
            await unitOfWork.SaveChangesAsync();

        }
        private async Task UpdatePaymentIntentFailed(string id)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetById(new OrderByPaymentIntentId(id));
            order.Status = OrderStatus.PaymentFailed;
            unitOfWork.GetRepository<Order, Guid>().Update(order);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

