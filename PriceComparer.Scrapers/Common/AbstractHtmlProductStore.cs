using PriceComparer.Domain.Products;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PriceComparer.Scrapers.Common
{
    public abstract class AbstractHtmlProductStore : IProductStore
    {
        public IEnumerable<Product> Find(string productName)
        {
            var products = GetProducts(productName);

            //return products.Where(p => p.Name.ToUpper().Contains(productName.ToUpper()));
            return products;
        }

        protected abstract IEnumerable<Product> GetProducts(string productName);

        protected string GetHtml(string url)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    return client.DownloadString(url);
                }
                catch (System.Exception)
                {
                    return string.Empty;
                }
            }
        }
    }
}