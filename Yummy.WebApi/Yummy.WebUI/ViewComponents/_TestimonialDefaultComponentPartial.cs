using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.TestimonialDtos;

namespace Yummy.WebUI.ViewComponents;

public class _TestimonialDefaultComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;
    public _TestimonialDefaultComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync($"http://localhost:5238/api/Testimonials/"); 
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultTestimonialDtos>>(jsonData);
            return View(values);
        }
        return View();
    }
}