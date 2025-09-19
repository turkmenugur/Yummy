using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.ReservationDtos;
using Yummy.WebUI.Models;

namespace Yummy.WebUI.ViewComponents.DashboardViewComponents;

public class _DashboardMainChartComponentPartial:ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DashboardMainChartComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("https://localhost:7038/");

        var response = await client.GetAsync("api/Reservations/GetReservationStats");
        var json = await response.Content.ReadAsStringAsync();

        var data = JsonConvert.DeserializeObject<List<ReservationChartDto>>(json);

        return View(data);
    }
}