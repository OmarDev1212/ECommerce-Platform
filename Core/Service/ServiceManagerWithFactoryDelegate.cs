using ServiceAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManagerWithFactoryDelegate(Func<IProductService> productFactory,
                                                    Func<IBasketService> basketFactory,
                                                    Func<IAuthenticationService> authFactory,
                                                    Func<IOrderService> orderFactory
                                                    ) : IServiceManager
    {
        public IProductService ProductService => productFactory.Invoke();

        public IBasketService BasketService => basketFactory.Invoke();

        public IAuthenticationService AuthenticationService => authFactory.Invoke();

        public IOrderService OrderService => orderFactory();
    }
}
