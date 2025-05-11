using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.ProductModule
{
    public class ProductQueryParameters
    {
        private const int DefaultSize = 5;
        private const int MaximumSize = 10;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions SortingOptions { get; set; }
        public string? SearchValue { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > 0 && value <= MaximumSize) ? value : DefaultSize;
        }

    }
}
