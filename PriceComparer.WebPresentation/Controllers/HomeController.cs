using PriceComparer.Domain.Products;
using PriceComparer.Scrapers.AH;
using PriceComparer.Scrapers.Jumbo;
using PriceComparer.Scrapers.Plus;
using PriceComparer.WebPresentation.Models;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace PriceComparer.WebPresentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string product)
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var finder = new ProductFinder();
                finder.AddProductStore(new JumboProductStore());
                finder.AddProductStore(new PlusProductStore());
                finder.AddProductStore(new AHProductStore());
                var model = new SearchViewModel();
                model.SearchTerm = product;
                model.Products = finder.Find(product).OrderBy(p => p.Price);

                stopwatch.Stop();

                model.SearchDuration = $"{stopwatch.Elapsed.Seconds} seconds and {stopwatch.Elapsed.Milliseconds} milliseconds";

                return View(model);
            }
            catch (System.Exception ex)
            {
                var model = new ErrorModel
                {
                    Exception = ex.ToString(),
                    Message = ex.Message,
                    Stacktrace = ex.StackTrace
                };

                return RedirectToAction("Index", "Error", model);
            }
        }
    }
}