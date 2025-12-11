using Microsoft.Maui.Controls;

namespace Converter
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private async void OnBackClicked(object? sender, System.EventArgs e)
        {
            // Navigate back to the main page
            await Shell.Current.GoToAsync("..");
        }
    }
}