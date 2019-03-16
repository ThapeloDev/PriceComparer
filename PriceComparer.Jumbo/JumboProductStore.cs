using PriceComparer.Domain.Products;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PriceComparer.Jumbo
{
    public class JumboProductStore : IProductStore
    {
        private readonly ProductScraper _scraper;

        public JumboProductStore()
        {
            _scraper = new ProductScraper();
        }

        public IEnumerable<Product> Find(string productName)
        {
            var products = new List<Product>();
            var html = GetHtml($"https://www.jumbo.com/zoeken?SearchTerm={productName}");

            var scrapedProducts = ScrapeProducts(html);

            products.AddRange(scrapedProducts);

            var pageNumber = 1;
            var loadProducts = true;

            while (loadProducts)
            {
                var url = BuildUrl(productName, pageNumber);
                html = GetHtml(url);

                scrapedProducts = ScrapeProducts(html);

                if (scrapedProducts.Any())
                    products.AddRange(scrapedProducts);
                else
                    loadProducts = false;

                pageNumber += 1;
            }

            return products;
        }

        private IEnumerable<Product> ScrapeProducts(string html)
        {
            return _scraper.Scrape(html);
        }

        private string GetHtml(string url)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        private string BuildUrl(string productName, int pageNumber)
        {
            return $"https://www.jumbo.com/producten?PageNumber={pageNumber}&SearchTerm={productName}";
        }
    }
}
