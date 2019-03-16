using PriceComparer.Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace PriceComparer.WebPresentation.Models
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            Products = Enumerable.Empty<Product>();
        }

        public string SearchTerm { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}