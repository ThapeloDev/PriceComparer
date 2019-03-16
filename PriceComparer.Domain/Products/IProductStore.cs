using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceComparer.Domain.Products
{
    public interface IProductStore
    {
        IEnumerable<Product> Find(string productName);
    }
}
