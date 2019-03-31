using PriceComparer.Domain.Products;
using PriceComparer.Scrapers.AH;
using PriceComparer.Scrapers.Jumbo;
using PriceComparer.Scrapers.Plus;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

namespace PriceComparer.API.Controllers.Products
{
    public class ProductController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Search(string keyword)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var finder = new ProductFinder();
            finder.AddProductStore(new JumboProductStore());
            finder.AddProductStore(new PlusProductStore());
            finder.AddProductStore(new AHProductStore());

            var model = new SearchResults();
            model.SearchTerm = keyword;
            model.Supermarkets = finder.Find(keyword).GroupBy(p => p.Supermarket).Select(s => new Supermarket
            {
                Name = s.Key,
                Products = s.ToList()
            });

            stopwatch.Stop();

            model.Duration = $"{stopwatch.Elapsed.Seconds} seconds and {stopwatch.Elapsed.Milliseconds} milliseconds";

            return Ok(model);
        }
    }
}