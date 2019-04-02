using System.Diagnostics;

namespace PriceComparer.Domain.Products
{
    [DebuggerDisplay("{Name}")]
    public class Product
    {
        private Product()
        {
        }

        public Product(string name, Price price, string imageUrl, string supermarket)
        {
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Supermarket = supermarket;
        }

        public Product(string name, Price price, string imageUrl, string supermarket, UnitSize unitSize)
        {
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Supermarket = supermarket;
            UnitSize = unitSize;
        }

        public string Name { get;  }

        public Price Price { get;  }

        public string ImageUrl { get;  }

        public string Supermarket { get; }

        public UnitSize UnitSize { get; }
    }
}
