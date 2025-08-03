using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.ViewComponents.AdminLayoutNavbarViewComponents;

public class _NavbarFormInlineAdminLayoutComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}