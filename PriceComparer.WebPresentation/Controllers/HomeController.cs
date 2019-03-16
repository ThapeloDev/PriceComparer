using PriceComparer.Domain.Products;
using PriceComparer.Jumbo;
using PriceComparer.Plus;
using PriceComparer.WebPresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var finder = new ProductFinder();
            finder.AddProductStore(new JumboProductStore());
            finder.AddProductStore(new PlusProductStore());

            var model = new SearchViewModel();
            model.SearchTerm = product;
            model.Products = finder.Find(product);

            return View(model);
        }
    }
}