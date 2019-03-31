using PriceComparer.WebPresentation.Models;
using System.Web.Mvc;

namespace PriceComparer.WebPresentation.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(ErrorModel model)
        {
            return View(model);
        }
    }
}