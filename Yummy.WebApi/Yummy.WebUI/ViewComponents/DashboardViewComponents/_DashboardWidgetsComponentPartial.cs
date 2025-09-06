﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.CategoryDtos;

namespace Yummy.WebUI.ViewComponents.DashboardViewComponents;

public class _DashboardWidgetsComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public _DashboardWidgetsComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        int r1, r2, r3, r4;
        Random rnd = new  Random();
        r1 = rnd.Next(1, 35);
        r2 = rnd.Next(1, 35);
        r3 = rnd.Next(1, 35);
        r4 = rnd.Next(1, 35);
        
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Reservations/GetTotalReservationCount");

        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        ViewBag.v1 = jsonData;
        ViewBag.r1 = r1;
     
        var client2 = _httpClientFactory.CreateClient();
        var responseMessage2 = await client2.GetAsync("http://localhost:5238/api/Reservations/GetTotalCustomerCount");
        var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
        ViewBag.v2 = jsonData2;
        ViewBag.r2 = r2;

        var client3 = _httpClientFactory.CreateClient();
        var responseMessage3 = await client3.GetAsync("http://localhost:5238/api/Reservations/GetPendingReservations");
        var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
        ViewBag.v3 = jsonData3;
        ViewBag.r3 = r3;

        var client4 = _httpClientFactory.CreateClient();
        var responseMessage4 = await client4.GetAsync("http://localhost:5238/api/Reservations/GetApprovedReservations");
        var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
        ViewBag.v4 = jsonData4;
        ViewBag.r4 = r4;
        
        //var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

        return View();
    }
}