using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace PriceComparer.SeleniumPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://www.ah.nl/zoeken?query=lays";
            var _chromeDriverPath = @"C:\Github\PriceComparer";

            var chromeOptions = new ChromeOptions();
            // chromeOptions.AddArgument("--headless");
            using (var driver = new ChromeDriver(_chromeDriverPath, chromeOptions))
            {
                var js = (IJavaScriptExecutor)driver;


                driver.Url = url;
                // Instruct the WebDriver to wait X seconds for elements to load
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

                //This will scroll the web page till end.		
                js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                
                //driver.Navigate();

                var html = driver.PageSource;

                File.WriteAllText(@"C:\Github\PriceComparer\PriceComparer.Scrapers\html.txt", html);
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
