using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents.AdminLayoutNavbarViewComponents;

public class _NavbarAccountMenuAdminLayoutComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}