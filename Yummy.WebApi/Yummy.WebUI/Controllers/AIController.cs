using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace Yummy.WebUI.Controllers;

public class AIController : Controller
{
    public IActionResult CreateRecipeWithOpenAI()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipeWithOpenAI(string prompt)
    {
        var apiKey = "";
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        var requestData= new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new {role="system",content="Sen, bir restoran için yemek önerileri yapabilen bir yapay zeka asistanısın. Amacımız, kullanıcı tarafından girilen malzemelere göre lezzetli ve yaratıcı yemek tarifleri sunmak."},
                new {role="user",content=prompt}
            },
            temperature = 0.5,
        };
        
        var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
            var content = result.choices[0].message.content;
            ViewBag.recipes = content;
        }
        else
        {
            ViewBag.recipes="Bir hata oluştu: " + response.ReasonPhrase;
            ViewBag.recipes="Bir hata oluştu: " + response.StatusCode;
        }

        return View();


    }
    public class OpenAIResponse
    {
        public List<Choice> choices { get; set; }
    }
    public class Choice
    {
        public Message message { get; set; }
    }
    
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
   
}