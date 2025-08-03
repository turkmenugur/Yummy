using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.MessageDtos;
using Yummy.WebUI.Dtos.NotificationDtos;

namespace Yummy.WebUI.ViewComponents.AdminLayoutNavbarViewComponents;

public class _NavbarNotificationAdminLayoutComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public _NavbarNotificationAdminLayoutComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"http://localhost:5238/api/Notifications");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultNotificationDto>>(jsonData);
            return View(values);
        }
        return View();
    }   
}