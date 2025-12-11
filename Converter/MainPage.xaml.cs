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
                if (sender is Button btn)
                {
                    var text = btn.Text?.Trim();
                    if (string.Equals(text, "Расстояние", StringComparison.OrdinalIgnoreCase))
                    {
                        await Shell.Current.GoToAsync(nameof(ConverterPage));
                        return;
                    }

                    if (string.Equals(text, "Масса", StringComparison.OrdinalIgnoreCase))
                    {
                        await Shell.Current.GoToAsync(nameof(MassConverterPage));
                        return;
                    }

                    if (string.Equals(text, "Температура", StringComparison.OrdinalIgnoreCase))
                    {
                        await Shell.Current.GoToAsync(nameof(TemperatureConverterPage));
                        return;
                    }

                    if (string.Equals(text, "Площадь", StringComparison.OrdinalIgnoreCase))
                    {
                        await Shell.Current.GoToAsync(nameof(AreaConverterPage));
                        return;
                    }

                    if (string.Equals(text, "Объем данных", StringComparison.OrdinalIgnoreCase) || string.Equals(text, "Объем данных", StringComparison.InvariantCultureIgnoreCase))
                    {
                        await Shell.Current.GoToAsync(nameof(DataVolumeConverterPage));
                        return;
                    }
                }

                // Default: open menu
                await Shell.Current.GoToAsync("MenuPage");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex}");
                await DisplayAlert("Error", "Не удалось открыть меню. Попробуйте ещё раз.", "OK");
            }
        }
    }
}
