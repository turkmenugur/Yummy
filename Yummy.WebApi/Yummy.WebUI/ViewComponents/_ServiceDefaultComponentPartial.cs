using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.ServiceDtos;

namespace Yummy.WebUI.ViewComponents;

public class _ServiceDefaultComponentPartial : ViewComponent
{
    
    private readonly IHttpClientFactory _httpClientFactory;
    
    public _ServiceDefaultComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"http://localhost:5238/api/Services/"); //istekte bulunacağımız adresi yazıyoruz
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);
            return View(values);
        }
        return View();
    }
}