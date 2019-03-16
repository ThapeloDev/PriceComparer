namespace PriceComparer.Domain.Products
{
    public class Product
    {
        public Product(string name, decimal price, string imageUrl, string supermarket)
        {
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Supermarket = supermarket;

        }

        public string Name { get;  }

        public decimal Price { get;  }

        public string ImageUrl { get;  }

        public string Supermarket { get; }
    }
}
