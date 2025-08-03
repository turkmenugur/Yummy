using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.WhyChooseUsDto;

namespace Yummy.WebUI.Controllers;

public class WhyChooseUsController : Controller
{
   private readonly IHttpClientFactory _httpClientFactory;

    public WhyChooseUsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> WhyChooseUsList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Services");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultWhyChooseUsDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateWhyChooseUs()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateWhyChooseUs(CreateWhyChooseUsDto createWhyChooseUsDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createWhyChooseUsDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5238/api/Services", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("WhyChooseUsList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteWhyChooseUs(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5238/api/Services?id="+ id);
        return RedirectToAction("WhyChooseUsList");
    }

    //Güncelleme işlemi için GetWhyChooseUs ve UpdateWhyChooseUs metodları
    [HttpGet]
    public async Task<IActionResult> UpdateWhyChooseUs(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Services/GetService?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<GetWhyChooseUsByIdDto>(jsonData);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateWhyChooseUs(UpdateWhyChooseUsDto updateWhyChooseUsDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateWhyChooseUsDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5238/api/Services/", stringContent);
        return RedirectToAction("WhyChooseUsList");
    }
}