using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractions
{
    public interface ICachingService    
    {
        //get
        Task<string?> GetAsync(string key);
        //set
        Task SetAsync(string key, object value, TimeSpan? expiration);
    }
}
