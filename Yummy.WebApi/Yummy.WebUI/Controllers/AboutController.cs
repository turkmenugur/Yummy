using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.AboutDtos;

namespace Yummy.WebUI.Controllers;

public class AboutController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AboutController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> AboutList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Abouts");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateAbout()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createAboutDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5238/api/Abouts", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("AboutList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteAbout(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5238/api/Abouts?id="+ id);
        return RedirectToAction("AboutList");
    }

    //Güncelleme işlemi için GetAbout ve UpdateAbout metodları
    [HttpGet]
    public async Task<IActionResult> UpdateAbout(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Abouts/GetAbout?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<GetAboutByIdDto>(jsonData);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateAboutDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5238/api/Abouts/", stringContent);
        return RedirectToAction("AboutList");
    }
}