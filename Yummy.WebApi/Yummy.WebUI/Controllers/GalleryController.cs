using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.ImageDtos;

namespace Yummy.WebUI.Controllers;

public class GalleryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GalleryController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<IActionResult> ImageList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Images");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultImageDto>>(jsonData);
            return View(values);
        }
        return View();
    }
}