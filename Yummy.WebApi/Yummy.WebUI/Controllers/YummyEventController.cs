using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.YummyEventDtos;

namespace Yummy.WebUI.Controllers;

public class YummyEventController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public YummyEventController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> YummyEventList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/YummyEvents");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultYummyEventDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateYummyEvent()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateYummyEvent(CreateYummyEventDto createYummyEventDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createYummyEventDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5238/api/YummyEvents", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("YummyEventList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteYummyEvent(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5238/api/YummyEvents?id="+ id);
        return RedirectToAction("YummyEventList");
    }

    //Güncelleme işlemi için GetYummyEvent ve UpdateYummyEvent metodları
    [HttpGet]
    public async Task<IActionResult> UpdateYummyEvent(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/YummyEvents/GetYummyEvent?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<GetYummyEventByIdDto>(jsonData);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateYummyEvent(UpdateYummyEventDto updateYummyEventDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateYummyEventDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5238/api/YummyEvents/", stringContent);
        return RedirectToAction("YummyEventList");
    }
}