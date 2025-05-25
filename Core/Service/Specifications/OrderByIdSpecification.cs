using DomainLayer.Models.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class OrderByIdSpecification:BaseSpecification<Order,Guid>
    {
        public OrderByIdSpecification(Guid id):base(o=>o.Id==id)
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
