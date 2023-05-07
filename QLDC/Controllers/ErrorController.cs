using Microsoft.AspNetCore.Mvc;

namespace QLDC.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(string error)
        {
            ViewBag.error = error;
            return View();
        }
    }
}
