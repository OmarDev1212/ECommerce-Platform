using DomainLayer.Models.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class OrdersByEmailSpecifcation : BaseSpecification<Order, Guid>
    {
        public OrdersByEmailSpecifcation(string email) : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
