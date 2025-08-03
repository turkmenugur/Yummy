using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents;

public class _NavbarDefaultComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}