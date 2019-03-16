using HtmlAgilityPack;
using PriceComparer.Domain.Products;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceComparer.Plus
{
    public class ProductScraper
    {
        public IEnumerable<Product> Scrape(string htmlSource)
        {
            var culture = new CultureInfo("en-US");
            var html = new HtmlDocument();
            html.LoadHtml(htmlSource);
            var products = new List<Product>();

            var collection = html.QuerySelectorAll("div.prod-tile");

            var count = collection.Count;

            foreach (var item in collection)
            {
                var title = item.Attributes["data-name"].Value;
                var imageUrl = item.QuerySelector("div.product-tile__img-container img").Attributes["src"].Value;
                var price = item.Attributes.GetValue<decimal>("data-price", culture);

                var product = new Product(title, price, imageUrl, "Plus");
                products.Add(product);
            }

            return products;
        }
    }
}
