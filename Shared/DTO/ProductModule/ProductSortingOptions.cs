using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.ProductModule
{
    [Flags]
    public enum ProductSortingOptions
    {
        NameAsc=1,
        NameDesc=2,
        PriceAsc=4,
        PriceDesc=8,
    }
}
