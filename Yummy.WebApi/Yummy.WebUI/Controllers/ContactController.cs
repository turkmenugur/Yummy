using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.ContactDtos;

namespace Yummy.WebUI.Controllers;

public class ContactController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ContactController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ContactList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Contacts");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateContact()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createContactDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5238/api/Contacts", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("ContactList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteContact(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5238/api/Contacts?id="+ id);
        return RedirectToAction("ContactList");
    }

    //Güncelleme işlemi için GetContact ve UpdateContact metodları
    [HttpGet]
    public async Task<IActionResult> UpdateContact(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Contacts/GetContact?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<GetContactByIdDto>(jsonData);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateContactDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5238/api/Contacts/", stringContent);
        return RedirectToAction("ContactList");
    }
}