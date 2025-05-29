using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderAggregate
{
    public class Order : BaseEntity<Guid>
    {
        public Order(string userEmail, ShippingAddress address, DeliveryMethod deliveryMethod, int deliveryMethodId, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
        {
            UserEmail = userEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            DeliveryMethodId = deliveryMethodId;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }
        public Order()
        {
            
        }
        public string UserEmail { get; set; }
        public ShippingAddress Address { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int DeliveryMethodId { get; set; }//[FK]
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        public OrderStatus Status { get; set; }=OrderStatus.Pending;
        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;
        public string PaymentIntentId { get; set; }
    }
}
