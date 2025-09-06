using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.Controllers;

public class DashboardController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}