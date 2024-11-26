using ClientManagementApp.Models;
using ClientManagementApp.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ClientManagementApp
{
    public partial class MainWindow : Window
    {
        private readonly ApiService _apiService = new();
        private bool _isAdmin;

        public MainWindow(bool isAdmin)
        {
            InitializeComponent();
            _isAdmin = isAdmin;

            if (!_isAdmin)
            {
                AddButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }

            LoadClients();
        }

        private async void LoadClients()
        {
            try
            {
                var clients = await _apiService.GetClientsAsync();
                ClientsDataGrid.ItemsSource = clients;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке клиентов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // добавление
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new Client { Name = "Введите ФИО", Email = "Введите email", Message = "Введите инфо" };
                await _apiService.AddClientAsync(client);
                LoadClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //редактирование
        private async void ClientsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (e.Row.Item is Client editedClient)
                {
                    await _apiService.UpdateClientAsync(editedClient.ID, editedClient);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientsDataGrid.SelectedItem is Client client)
                {
                    await _apiService.UpdateClientAsync(client.ID, client);
                    LoadClients();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите клиента для обновления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //удаление
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientsDataGrid.SelectedItem is Client client)
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить клиента \"{client.Name}\"?",
                                                 "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        await _apiService.DeleteClientAsync(client.ID);
                        LoadClients();
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите клиента для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
