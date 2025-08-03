using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.CategoryDtos;

namespace Yummy.WebUI.Controllers;

public class CategoryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CategoryController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> CategoryList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Categories");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createCategoryDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5238/api/Categories", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("CategoryList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteCategory(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5238/api/Categories?id="+ id);
        return RedirectToAction("CategoryList");
    }

    //Güncelleme işlemi için GetCategory ve UpdateCategory metodları
    [HttpGet]
    public async Task<IActionResult> UpdateCategory(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Categories/GetCategory?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<GetCategoryByIdDto>(jsonData);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5238/api/Categories/", stringContent);
        return RedirectToAction("CategoryList");
    }
}