using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.ReservationDtos;

namespace Yummy.WebUI.Controllers;

public class ReservationController : Controller
{
   private readonly IHttpClientFactory _httpClientFactory;

    public ReservationController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ReservationList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Reservations");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultReservationDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateReservation()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateReservation(CreateReservationDto createReservationDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createReservationDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5238/api/Reservations", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("ReservationList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteReservation(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5238/api/Reservations?id="+ id);
        return RedirectToAction("ReservationList");
    }

    //Güncelleme işlemi için GetReservation ve UpdateReservation metodları
    [HttpGet]
    public async Task<IActionResult> UpdateReservation(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Reservations/GetReservation?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<GetReservationByIdDto>(jsonData);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateReservation(UpdateReservationDto updateReservationDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateReservationDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5238/api/Reservations/", stringContent);
        return RedirectToAction("ReservationList");
    }
}