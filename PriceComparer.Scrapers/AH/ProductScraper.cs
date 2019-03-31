using HtmlAgilityPack;
using PriceComparer.Domain;
using PriceComparer.Domain.Products;
using PriceComparer.Scrapers.Common;
using PriceComparer.Scrapers.Equalisers.Size;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PriceComparer.Scrapers.AH
{
    public class ProductScraper : AbstractProductScraper
    {
        public IEnumerable<Product> Scrape(string htmlSource)
        {
            if (string.IsNullOrEmpty(htmlSource))
                return Enumerable.Empty<Product>();

            var culture = new CultureInfo("en-US");
            var html = new HtmlDocument();
            html.LoadHtml(htmlSource);
            var products = new List<Product>();

            var collection = html.QuerySelectorAll("div.productcard__container");

            var count = collection.Count;

            foreach (var item in collection)
            {
                var title = item.QuerySelector("span.line-clamp").InnerText;
                var imageUrl = item.QuerySelector("img.productcard__image")?.Attributes["src"].Value;
                var price = GetPrice(item);
                var unitSize = GetUnitSize(item.QuerySelector("span.product-unit-size").InnerText);

                var product = new Product(title, price, imageUrl, "AH", unitSize);
                products.Add(product);
            }

            return products;
        }

        private decimal GetPrice(HtmlNode item)
        {
            var integer = item.QuerySelector("span.price__integer").InnerText;
            var fractional = item.QuerySelector("span.price__fractional").InnerText;

            var concatPrice = $"{integer}.{fractional}";
            var price = concatPrice.ChangeType<decimal>();

            return price;
        }
    }
}
