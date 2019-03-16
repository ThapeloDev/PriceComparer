using PriceComparer.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PriceComparer.Plus
{
    public class PlusProductStore : IProductStore
    {
        public IEnumerable<Product> Find(string productName)
        {
            var html = GetHtml($"https://www.plus.nl/zoekresultaten?SearchTerm={productName}");
            var scraper = new ProductScraper();

            var products = scraper.Scrape(html);

            return products;

        }

        private string GetHtml(string url)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}
