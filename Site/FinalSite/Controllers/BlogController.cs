using Microsoft.AspNetCore.Mvc;
using FinalSite.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinalSite.Controllers
{
    public class BlogController : Controller
    {
        private readonly HttpClient _httpClient;

        public BlogController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ClientManagementAPI");
        }

        public async Task<IActionResult> Blog()
        {
            try
            {
                var response = await _httpClient.GetAsync("blogposts");
                Console.WriteLine($"Отправлен запрос: {response.RequestMessage?.RequestUri}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}, {response.ReasonPhrase}");
                    ViewBag.ErrorMessage = $"Ошибка при запросе данных: {response.ReasonPhrase}";
                    return View(new List<BlogPost>());
                }

                var json = await response.Content.ReadAsStringAsync();

                var blogPosts = JsonSerializer.Deserialize<List<BlogPost>>(json);

                return View(blogPosts ?? new List<BlogPost>());
            }
            catch (Exception ex)
            {
                return View(new List<BlogPost>());
            }
        }
    }
}
