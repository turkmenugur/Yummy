using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.FeatureDtos;

namespace Yummy.WebUI.Controllers;

public class FeatureController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FeatureController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> FeatureList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Features");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateFeature()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createFeatureDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5238/api/Features", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("FeatureList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteFeature(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5238/api/Features?id="+ id);
        return RedirectToAction("FeatureList");
    }

    //Güncelleme işlemi için GetFeature ve UpdateFeature metodları
    [HttpGet]
    public async Task<IActionResult> UpdateFeature(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Features/GetFeature?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<GetFeatureByIdDto>(jsonData);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateFeatureDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5238/api/Features/", stringContent);
        return RedirectToAction("FeatureList");
    }
}