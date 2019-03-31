using HtmlAgilityPack;
using PriceComparer.Domain.Products;
using PriceComparer.Scrapers.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace PriceComparer.Scrapers.Jumbo
{
    public class ProductScraper : AbstractProductScraper
    {
        public IEnumerable<Product> Scrape(string htmlSource)
        {
            var culture = new CultureInfo("en-US");
            var html = new HtmlDocument();
            html.LoadHtml(htmlSource);
            var products = new List<Product>();

            var collection = html.QuerySelectorAll("div.jum-item-product");

            var count = collection.Count;

            foreach (var item in collection)
            {
                var title = item.QuerySelector("dt.jum-item-title a").InnerText;
                var imageUrl = WebUtility.HtmlDecode(item.QuerySelector("dd.jum-item-figure img").Attributes["data-jum-src"].Value);
                var price = item.QuerySelector("dd.jum-item-price input[jum-data-price]").Attributes.GetValue<decimal>("jum-data-price", culture);
                var unitSize = GetUnitSize(item.QuerySelector("span.jum-pack-size")?.InnerText);

                var product = new Product(title, price, imageUrl, "Jumbo", unitSize);
                products.Add(product);
            }

            return products;
        }

        public bool HasProductsToScrape(string htmlSource)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlSource);

            return html.QuerySelector("div.jum-item-product") != null;
        }
    }
}
