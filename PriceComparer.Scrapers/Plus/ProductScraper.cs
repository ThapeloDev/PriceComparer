using HtmlAgilityPack;
using PriceComparer.Domain.Products;
using PriceComparer.Scrapers.Common;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PriceComparer.Scrapers.Plus
{
    public class ProductScraper : AbstractProductScraper
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
                var productId = item.Attributes["data-id"].Value;
                var imageUrl = $"https://www.plus.nl/INTERSHOP/static/WFS/PLUS-Site/-/PLUS/nl_NL/product/M/{productId}.png";
                var price = item.Attributes.GetValue<decimal>("data-price", culture);
                var unitSize = GetUnitSize(item.QuerySelector("span.product-tile__quantity")?.InnerText);

                var product = new Product(title, price, imageUrl, "Plus", unitSize);
                products.Add(product);
            }

            return products;
        }
    }
}
