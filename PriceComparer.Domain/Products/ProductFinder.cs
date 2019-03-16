using System.Collections.Generic;

namespace PriceComparer.Domain.Products
{
    public class ProductFinder
    {
        private readonly List<IProductStore> _productStores;

        public ProductFinder()
        {
            _productStores = new List<IProductStore>();

        }

        public void AddProductStore(IProductStore store)
        {
            _productStores.Add(store);
        }

        public IEnumerable<Product> Find(string product)
        {
            var products = new List<Product>();

            foreach (var store in _productStores)
            {
                var results = store.Find(product);

                products.AddRange(results);
            }

            return products;
        }
    } 
}
