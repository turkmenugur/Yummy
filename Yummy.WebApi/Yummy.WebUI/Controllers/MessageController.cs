using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yummy.WebUI.Dtos.MessageDtos;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Yummy.WebUI.Controllers;

public class MessageController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MessageController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> MessageList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Messages");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateMessage()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createMessageDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5238/api/Messages", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("MessageList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteMessage(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5238/api/Messages?id="+ id);
        return RedirectToAction("MessageList");
    }

    //Güncelleme işlemi için GetMessage ve UpdateMessage metodları
    [HttpGet]
    public async Task<IActionResult> UpdateMessage(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Messages/GetMessage?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<GetMessageByIdDto>(jsonData);
        return View(value);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateMessage(UpdateMessageDto updateMessageDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateMessageDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5238/api/Messages/", stringContent);
        return RedirectToAction("MessageList");
    }
    
    public async Task<IActionResult> AnswerMessageWithOpenAI(int id, string prompt)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5238/api/Messages/GetMessage?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<GetMessageByIdDto>(jsonData);
        prompt = value.MessageDetails;
        
        var apiKey = "";
        using var client2 = new HttpClient();
        client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        var requestData= new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new {role="system",content="Sen, bir restoran için kullanıcıların göndermiş oldukları" +
                                           " mesajları detaylı ve olabildiğince olumlu, müşteri memnuniyeti gözeten cevaplar veren " +
                                           "bir yapay zeka asistanısın. " +
                                           "Amacımız, kullanıcı tarafından gönderilen mesajlara en olumlu ve en mantıklı cevapları sunmak."},
                new {role="user",content=prompt}
            },
            temperature = 0.5,
        };
        
        var response = await client2.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<AIController.OpenAIResponse>();
            var content = result.choices[0].message.content;
            ViewBag.answerAI = content;
        }
        else
        {
            ViewBag.answerAI="Bir hata oluştu: " + response.ReasonPhrase;
            ViewBag.answerAI="Bir hata oluştu: " + response.StatusCode;
        }
        
        return View(value);
    }

    public PartialViewResult SendMessage()
    {
        return PartialView();
    }
    
    [HttpPost]
    public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
    {

        var client = new HttpClient();
        var apiKey = "";
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        try
        {
            var tranlateRequestBody = new
            {
                inputs = createMessageDto.MessageDetails
            };
            var translateJson = JsonSerializer.Serialize(tranlateRequestBody);
            var translateContent = new StringContent(translateJson, Encoding.UTF8, "application/json");
            var translateResponse = await client.PostAsync("https://api-inference.huggingface.co/models/Helsinki-NLP/opus-mt-tr-en", translateContent);
            var translateResponseString = await translateResponse.Content.ReadAsStringAsync();

            string englishText = createMessageDto.MessageDetails;
            if (translateResponseString.TrimStart().StartsWith("["))
            {
                var translateDoc = JsonDocument.Parse(translateResponseString);
                englishText = translateDoc.RootElement[0].GetProperty("translation_text").GetString();
                //ViewBag.v = englishText;
            }
            var toxicRequestBody = new
            {
                inputs = englishText
            };
            var toxicJson = JsonSerializer.Serialize(toxicRequestBody);
            var toxicContent = new StringContent(toxicJson, Encoding.UTF8, "application/json");
            var toxicResponse = await client.PostAsync("https://api-inference.huggingface.co/models/unitary/toxic-bert", toxicContent);
            var toxicResponseString = await toxicResponse.Content.ReadAsStringAsync();

            if (toxicResponseString.TrimStart().StartsWith("["))
            {
                var toxicDoc = JsonDocument.Parse(toxicResponseString);
                foreach (var item in toxicDoc.RootElement.EnumerateArray())
                {
                    string label = item.GetProperty("label").GetString();
                    double score = item.GetProperty("score").GetDouble();
                    
                    if (score > 0.5)
                    {
                        createMessageDto.Status = "Toksik Mesaj";
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(createMessageDto.Status))
            {
                createMessageDto.Status = "Mesaj Alındı";
            }
        }
        catch(Exception ex)
        {
            
            createMessageDto.Status = "Onay Bekliyor";
        }
        
        var client2 = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createMessageDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client2.PostAsync("http://localhost:5238/api/Messages", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("MessageList");
        }
        return View();
    }
}