using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        // GET: DefaultController
        public ActionResult Index()
        {
            return View();
        }

    }
}
