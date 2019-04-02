using HtmlAgilityPack;
using PriceComparer.Domain;
using PriceComparer.Domain.Products;
using PriceComparer.Scrapers.Common;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PriceComparer.Scrapers.Plus
{
    public class ProductScraper : AbstractProductScraper
    {
        private readonly CultureInfo _enCulture;

        public ProductScraper()
        {
            _enCulture = new CultureInfo("en-US");
        }

        public IEnumerable<Product> Scrape(string htmlSource)
        {
            
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
                var price = GetPrice(item);
                var unitSize = GetUnitSize(item.QuerySelector("span.product-tile__quantity")?.InnerText);

                var product = new Product(title, price, imageUrl, "Plus", unitSize);
                products.Add(product);
            }

            return products;
        }

        private Price GetPrice(HtmlNode item)
        {
            var price = item.Attributes.GetValue<decimal>("data-price", _enCulture);

            if(!HasOffer(item))
                return new Price(price);

            if (IsDescriptiveDiscount(item))
            {
                var offerDescription = GetOfferDescription(item);

                return new Price(price, offerDescription);
            }

            var offerPriceBox = item.QuerySelector("div.text-clover");
            var integer = offerPriceBox.ChildNodes[0].InnerText.StripSpecialChars();
            var fractional = offerPriceBox.ChildNodes[1].InnerText.StripSpecialChars();

            var concatPrice = $"{integer}.{fractional}";
            var offerPrice = concatPrice.ChangeType<decimal>();

            return new Price(price, offerPrice);
        }

        private bool IsDescriptiveDiscount(HtmlNode item)
        {
            return item.QuerySelector("div.clover-size-1") != null;
        }

        private string GetOfferDescription(HtmlNode item)
        {
            return item.QuerySelector("div.clover-size-1").InnerText;
        }

        private bool HasOffer(HtmlNode item)
        {
            return item.QuerySelector("div.product-tile__price--discount") != null;
        }
    }
}
