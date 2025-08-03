using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.TestimonialDtos;

namespace Yummy.WebUI.Controllers;

public class TestimonialController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TestimonialController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> TestimonialList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Testimonials");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultTestimonialDtos>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateTestimonial()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createTestimonialDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5238/api/Testimonials", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("TestimonialList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteTestimonial(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5238/api/Testimonials?id="+ id);
        return RedirectToAction("TestimonialList");
    }

    //Güncelleme işlemi için GetTestimonial ve UpdateTestimonial metodları
    [HttpGet]
    public async Task<IActionResult> UpdateTestimonial(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Testimonials/GetTestimonial?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<GetTestimonialByIdDto>(jsonData);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateTestimonialDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5238/api/Testimonials/", stringContent);
        return RedirectToAction("TestimonialList");
    }
}