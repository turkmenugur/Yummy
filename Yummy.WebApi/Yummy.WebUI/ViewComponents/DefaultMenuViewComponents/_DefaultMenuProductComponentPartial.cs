using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.ProductDtos;

namespace Yummy.WebUI.ViewComponents.DefaultMenuViewComponents;

public class _DefaultMenuProductComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    public _DefaultMenuProductComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"http://localhost:5238/api/Products/"); 
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
            return View(values);
        }
        return View();
    }
}