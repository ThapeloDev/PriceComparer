using PriceComparer.Domain.Products;
using System.Collections.Generic;

namespace PriceComparer.API.Controllers.Products
{
    public class Supermarket
    {
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}