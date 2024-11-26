using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using ClientManagementApp.Models;

namespace ClientManagementApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7282/api/") 
            };
        }

       public HttpClient GetHttpClient()
            { return _httpClient; }

       public async Task<List<Client>> GetClientsAsync()
        {
            var response = await _httpClient.GetAsync("Clients");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Client>>(content);
        }

        public async Task AddClientAsync(Client client)
        {
            var json = JsonSerializer.Serialize(client);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Clients", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateClientAsync(int id, Client client)
        {
            var json = JsonSerializer.Serialize(client);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Clients/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteClientAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Clients/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
