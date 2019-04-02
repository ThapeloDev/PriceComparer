using HtmlAgilityPack;
using PriceComparer.Domain.Products;
using PriceComparer.Scrapers.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Linq;
using PriceComparer.Domain;

namespace PriceComparer.Scrapers.Jumbo
{
    public class ProductScraper : AbstractProductScraper
    {
        private readonly CultureInfo _usCulture;
        private readonly CultureInfo _nlCulture;

        public ProductScraper()
        {
            _usCulture = new CultureInfo("en-US");
            _nlCulture = new CultureInfo("nl-NL");
        }

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
                var price = GetPrice(item);
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

        private Price GetPrice(HtmlNode node)
        {
            var price = node
                .QuerySelector("dd.jum-item-price input[jum-data-price]")
                .Attributes
                .GetValue<decimal>("jum-data-price", _usCulture);

            if (!HasOffer(node))
                return new Price(price);

            var regularPrice = GetRegularPriceWhenOfferApplied(node);

            if(regularPrice > 0)
                return new Price(regularPrice, price);

            var offerDescription = GetOfferDescription(node);

            return new Price(price, offerDescription);
        }

        private string GetOfferDescription(HtmlNode node)
        {
            if (!HasOffer(node))
                return string.Empty;

            return node
                .QuerySelector("dd.jum-item-badges img")
                ?.Attributes["alt"]
                ?.Value
                ?? string.Empty;
        }

        private decimal GetRegularPriceWhenOfferApplied(HtmlNode node)
        {
            if (!HasOffer(node))
                return 0;

            return node
                .QuerySelector("dd.jum-item-price span.jum-was-price")
                ?.InnerText
                ?.ChangeType<decimal>(_nlCulture)
                ?? 0;
        }

        private bool HasOffer(HtmlNode node)
        {
            return node
                .QuerySelector("div.jum-promotion-date")
                ?.HasChildNodes 
                ?? false;
        }
    }
}
