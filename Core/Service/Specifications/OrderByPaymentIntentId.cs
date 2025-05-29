using DomainLayer.Models.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class OrderByPaymentIntentId:BaseSpecification<Order,Guid>
    {
        public OrderByPaymentIntentId(string paymentIntentId):base(o=>o.PaymentIntentId== paymentIntentId)
        {
            
        }
    }
}
