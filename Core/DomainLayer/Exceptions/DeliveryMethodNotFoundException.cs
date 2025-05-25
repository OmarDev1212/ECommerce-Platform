using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class DeliveryMethodNotFoundException(int id):NotFoundException($"No Delivery method with Id:{id}")
    {
    }
}
