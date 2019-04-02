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

        private Price GetPrice(HtmlNode item)
        {
            if (!HasOffer(item))
            {
                var priceLabel = item.QuerySelector("div.price-label");
                var price = ExtractPrice(priceLabel);
                return new Price(price);
            }

            var offerPriceDiv = item.QuerySelector("div.price--bonus");
            var offerPrice = ExtractPrice(offerPriceDiv);

            var regularPriceDiv = item.QuerySelector("div.price-label__was");
            var regularPrice = ExtractPrice(regularPriceDiv);

            return new Price(regularPrice, offerPrice);
        }

        private decimal ExtractPrice(HtmlNode offerPriceDiv)
        {
            var integer = offerPriceDiv.QuerySelector("span.price__integer").InnerText;
            var fractional = offerPriceDiv.QuerySelector("span.price__fractional").InnerText;

            var concatPrice = $"{integer}.{fractional}";
            var price = concatPrice.ChangeType<decimal>();

            return price;
        }

        private bool HasOffer(HtmlNode item)
        {
            return item.QuerySelector("div.price-label__was") != null;
        }
    }
}
