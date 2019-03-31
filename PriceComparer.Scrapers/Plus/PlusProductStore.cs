using PriceComparer.Domain.Products;
using PriceComparer.Scrapers.Common;
using System.Collections.Generic;

namespace PriceComparer.Scrapers.Plus
{
    public class PlusProductStore : AbstractHtmlProductStore
    {
        protected override IEnumerable<Product> GetProducts(string productName)
        {
            var html = GetHtml($"https://www.plus.nl/zoekresultaten?SearchTerm={productName}");
            var scraper = new ProductScraper();

            var products = scraper.Scrape(html);

            return products;

        }
    }
}
