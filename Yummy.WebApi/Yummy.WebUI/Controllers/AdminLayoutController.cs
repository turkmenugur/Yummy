using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.Controllers;

public class AdminLayoutController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}