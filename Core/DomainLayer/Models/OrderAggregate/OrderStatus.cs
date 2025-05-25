using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderAggregate
{
    [Flags]
    public enum OrderStatus:byte
    {
        Pending=1,
        PaymentRecieved=2,
        PaymentFailed=4
    }
}
