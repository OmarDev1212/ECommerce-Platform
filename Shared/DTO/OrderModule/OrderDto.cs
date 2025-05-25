using Shared.DTO.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.OrderModule
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string UserEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public IEnumerable<OrderItemDto> Items { get; set; } = [];
        public AddressDto  Address  { get; set; }
        public string DeliveryMethod { get; set; }
        public string Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
