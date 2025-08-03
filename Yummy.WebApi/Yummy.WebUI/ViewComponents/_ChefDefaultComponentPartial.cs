using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.ChefDtos;

namespace Yummy.WebUI.ViewComponents;

public class _ChefDefaultComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public _ChefDefaultComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"http://localhost:5238/api/Chefs/"); //istekte bulunacağımız adresi yazıyoruz
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultChefDto>>(jsonData);
            return View(values);
        }
        return View();
    }
}