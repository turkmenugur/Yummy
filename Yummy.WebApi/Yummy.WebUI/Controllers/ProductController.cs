using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.CategoryDtos;
using Yummy.WebUI.Dtos.ProductDtos;

namespace Yummy.WebUI.Controllers;

public class ProductController : Controller
{
   private readonly IHttpClientFactory _httpClientFactory;

    public ProductController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ProductList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Products/ProductListWithCategory");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Categories");
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
        List<SelectListItem> categoryValues = (from x in values
                                       select new SelectListItem
                                       {
                                           Text = x.CategoryName,
                                           Value = x.CategoryId.ToString()
                                       }).ToList();
        ViewBag.v = categoryValues;
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createProductDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5238/api/Products/CreateProductWithCategory", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("ProductList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteProduct(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5238/api/Products?id="+ id);
        return RedirectToAction("ProductList");
    }

    //Güncelleme işlemi için GetProduct ve UpdateProduct metodları
    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Products/GetProducts?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<GetProductByIdDto>(jsonData);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateProductDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5238/api/Products/", stringContent);
        return RedirectToAction("ProductList");
    }
}