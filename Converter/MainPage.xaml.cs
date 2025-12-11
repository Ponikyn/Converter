using System;

namespace Converter
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSquareButtonClicked(object? sender, EventArgs e)
        {
            try
            {
                // Use Shell route string to navigate; ensure route is registered in AppShell
                await Shell.Current.GoToAsync("MenuPage");
            }
            catch (Exception ex)
            {
                // Log and show a simple alert so the app doesn't crash silently
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex}");
                await DisplayAlert("Error", "Не удалось открыть меню. Попробуйте ещё раз.", "OK");
            }
        }
    }
}
