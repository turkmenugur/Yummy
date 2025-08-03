using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos;
using Yummy.WebUI.Dtos.ChefDtos;

namespace Yummy.WebUI.Controllers;

public class ChefController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ChefController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ChefList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Chefs");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultChefDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateChef()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateChef(CreateChefDto createChefDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createChefDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5238/api/Chefs", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("ChefList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteChef(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5238/api/Chefs?id="+ id);
        return RedirectToAction("ChefList");
    }

    //Güncelleme işlemi için GetChef ve UpdateChef metodları
    [HttpGet]
    public async Task<IActionResult> UpdateChef(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Chefs/GetChef?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<GetChefByIdDto>(jsonData);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateChef(UpdateChefDto updateChefDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateChefDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5238/api/Chefs/", stringContent);
        return RedirectToAction("ChefList");
    }
}