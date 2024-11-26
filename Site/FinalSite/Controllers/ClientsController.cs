using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FinalSite.Models;

namespace FinalSite.Controllers
{
    public class ClientsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ClientsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ClientManagementAPI");
        }

        // Отображение формы
        public IActionResult Create()
        {
            return View();
        }

        // Обработка данных формы
        [HttpPost]
        public async Task<IActionResult> Create(ClientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Пожалуйста, заполните все поля корректно.";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("clients", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Клиент успешно добавлен!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = $"Ошибка: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Исключение: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
