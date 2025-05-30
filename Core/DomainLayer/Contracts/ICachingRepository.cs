using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ICachingRepository
    {
        //get
        Task<string?> GetAsync(string key);
        //set
        Task SetAsync(string key, string value, TimeSpan? expiration = null);
    }
}
