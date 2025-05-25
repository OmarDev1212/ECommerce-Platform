using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.BasketModule;
using ServiceAbstractions;
using Shared.DTO.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<CustomerBasketDto?> CreateOrUpdateBasket(CustomerBasketDto basketDto)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basketDto);
            var Createdbasket = await _basketRepository.CreateOrUpdateBasket(mappedBasket);
            return _mapper.Map<CustomerBasket, CustomerBasketDto>(Createdbasket);
        }

        public async Task<bool> DeleteBasket(string basketId) => await _basketRepository.DeleteBasket(basketId);

        public async Task<CustomerBasketDto?> GetCustomerBasket(string basketId)
        {
            var basket = await _basketRepository.GetCustomerBasket(basketId);
            return _mapper.Map<CustomerBasket, CustomerBasketDto>(basket);

        }

    }
}
