using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.CategoryDtos;

namespace Yummy.WebUI.ViewComponents.DefaultMenuViewComponents;

public class _DefaultMenuCategoryComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    public _DefaultMenuCategoryComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"http://localhost:5238/api/Categories/"); 
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            return View(values);
        }
        return View();
    }
}