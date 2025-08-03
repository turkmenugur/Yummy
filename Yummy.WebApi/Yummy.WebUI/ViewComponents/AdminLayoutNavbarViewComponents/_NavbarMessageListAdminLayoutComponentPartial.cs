using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.ChefDtos;
using Yummy.WebUI.Dtos.MessageDtos;

namespace Yummy.WebUI.ViewComponents.AdminLayoutNavbarViewComponents;

public class _NavbarMessageListAdminLayoutComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public _NavbarMessageListAdminLayoutComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"http://localhost:5238/api/Messages/MessageListByIsReadFalse");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultMessageByIsReadFalseDto>>(jsonData);
            return View(values);
        }
        return View();
    }   
}