using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace ClientManagementApp
{
    public partial class BlogWindow : Window
    {
        private readonly HttpClient _httpClient;

        public BlogWindow(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var blogPost = new
                {
                    DatePosted = DateTime.Now,
                    Content = ContentTextBox.Text
                };

                var json = JsonSerializer.Serialize(blogPost);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("blogposts", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Запись успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    ContentTextBox.Clear();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Ошибка при добавлении записи: {response.StatusCode}\n{error}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

