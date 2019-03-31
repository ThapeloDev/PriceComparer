using PriceComparer.Domain;
using PriceComparer.Domain.Products;
using PriceComparer.Scrapers.Common;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PriceComparer.Scrapers.Jumbo
{
    public class JumboProductStore : AbstractHtmlProductStore
    {
        private readonly ProductScraper _scraper;

        public JumboProductStore()
        {
            _scraper = new ProductScraper();
        }

        protected override IEnumerable<Product> GetProducts(string productName)
        {
            var products = new ConcurrentBag<Product>();
            var tasks = new List<Task>();

            var mainPageTask = Task.Run(() =>
            {
                var html = GetHtml($"https://www.jumbo.com/zoeken?SearchTerm={productName}");

                var scrapedProducts = ScrapeProducts(html);

                products.AddRange(scrapedProducts);
            });

            tasks.Add(mainPageTask);

            var pageCount = 3; // DeterminePageCount(productName);

            if (pageCount == 0)
                return products;

            Parallel.For(1, pageCount, pageNumber => 
            {
                var task = Task.Run(() =>
                {
                    var url = BuildUrl(productName, pageNumber);
                    var html = GetHtml(url);

                    var scrapedProducts = ScrapeProducts(html);

                    products.AddRange(scrapedProducts);
                });

                tasks.Add(task);
            });

            Task.WaitAll(tasks.ToArray());

            return products;
        }

        private int DeterminePageCount(string productName)
        {
            var pageNumber = new PageNumber();

            while (!pageNumber.Determined)
            {
                var url = BuildUrl(productName, pageNumber.Current);
                var html = GetHtml(url);

                if (_scraper.HasProductsToScrape(html))
                    pageNumber.Increase();
                else
                    pageNumber.Decrease();
            }

            return pageNumber.Current;
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
