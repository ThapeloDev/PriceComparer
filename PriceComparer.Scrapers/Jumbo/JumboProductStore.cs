using PriceComparer.Domain;
using PriceComparer.Domain.Products;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PriceComparer.Scrapers.Jumbo
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

        private class PageNumber
        {
            private List<int> _processedPageNumbers;
            private bool _isIncreasedLastTime;

            public PageNumber()
            {
                _isIncreasedLastTime = true;
                _processedPageNumbers = new List<int>() { 5 };

                Current = 5;
                Determined = false;
            }

            public int Current { get; private set; }

            public bool Determined { get; private set; }

            public void Increase()
            {
                if (_isIncreasedLastTime)
                    Current += 5;
                else
                    Current += 1;

                _processedPageNumbers.Add(Current);

            }

            public void Decrease()
            {
                _processedPageNumbers.Remove(Current);

                if (Current > 2)
                    Current -= 2;
                else
                    Current -= 1;

                if (Current <= 0)
                {
                    Determined = true;
                }
                if (_processedPageNumbers.Contains(Current))
                {
                    Determined = true;
                }
                if (_processedPageNumbers.Any(p => p > Current))
                {
                    Determined = true;
                    Current = _processedPageNumbers.Where(p => p > Current).First();
                }
                else
                {
                    _processedPageNumbers.Add(Current);

                    _isIncreasedLastTime = false;
                }
            }
        }
    }
}
