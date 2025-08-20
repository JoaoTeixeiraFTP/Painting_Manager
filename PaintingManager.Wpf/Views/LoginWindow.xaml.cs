using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace PaintingManager.Wpf.Views;

public partial class LoginWindow : Window
{
    private readonly HttpClient _http = new() { BaseAddress = new Uri("http://localhost:5000") };

    public LoginWindow()
    {
        InitializeComponent(); // Agora vai compilar porque o XAML gera este método
    }

    private async void Login_Click(object sender, RoutedEventArgs e)
    {
        var resp = await _http.PostAsJsonAsync("/login", new
        {
            Username = UsernameBox.Text,
            Password = PasswordBox.Password
        });

        if (!resp.IsSuccessStatusCode)
        {
            ErrorText.Text = "⚠️ Login inválido!";
            return;
        }

        var data = await resp.Content.ReadFromJsonAsync<LoginResponse>();
        if (data == null) { ErrorText.Text = "Erro inesperado"; return; }

        Application.Current.Properties["jwt"] = data.Token;
        Application.Current.Properties["role"] = data.Role;

        var main = new MainWindow();
        main.Show();
        this.Close();
    }
}

public class LoginResponse
{
    public string Token { get; set; } = "";
    public string Role { get; set; } = "";
}
