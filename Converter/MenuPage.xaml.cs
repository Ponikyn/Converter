using Microsoft.Maui.Controls;
using System;

namespace Converter
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private async void OnBackClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnConverterMenuClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ConverterPage));
        }
    }
}