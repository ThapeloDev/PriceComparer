using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PriceComparer.Domain.Products;
using PriceComparer.Scrapers.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace PriceComparer.Scrapers.AH
{
    public class AHProductStore : AbstractHtmlProductStore
    {
        private readonly ProductScraper _scraper;
        private string _chromeDriverPath;

        public AHProductStore()
        {
            _scraper = new ProductScraper();
            _chromeDriverPath = ConfigurationManager.AppSettings["ChromeDriverPath"];
        }

        protected override IEnumerable<Product> GetProducts(string productName)
        {
            var url = $"https://www.ah.nl/zoeken?query={productName}";
            var html = GetHtml(url);

            var products = _scraper.Scrape(html);
            return products;

            //var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("--headless");
            //using (var driver = new ChromeDriver(_chromeDriverPath, chromeOptions))
            //{
            //    driver.Url = url;
            //    driver.Navigate();

            //    var html = driver.PageSource;

            //    var products = _scraper.Scrape(html);
            //    return products;
            //}



            // Enumerable.Empty<Product>();
        }

        private string GetHtmlSource()
        {
            var htmlGetterPath = @"C:\Github\PriceComparer\PriceComparer.SeleniumPOC\bin\Debug\PriceComparer.SeleniumPOC.exe";

            var process = new Process();
            var processInfo = new ProcessStartInfo(htmlGetterPath);
            processInfo.UseShellExecute = false;

            process.StartInfo = processInfo;
            
            process.Start();
            process.WaitForExit();

            var html = File.ReadAllText(@"C:\Github\PriceComparer\PriceComparer.Scrapers\html.txt");
            return html;
        }
    }

}
