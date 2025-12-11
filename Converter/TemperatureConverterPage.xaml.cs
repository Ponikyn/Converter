using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Converter
{
    public partial class TemperatureConverterPage : ContentPage
    {
        // We'll handle temperature conversions specially (not linear scaling like length/mass)
        readonly string[] units = new[] { "Celsius", "Fahrenheit", "Kelvin" };

        public TemperatureConverterPage()
        {
            InitializeComponent();

            foreach (var u in units)
            {
                SourceUnitPicker.Items.Add(u);
                TargetUnitPicker.Items.Add(u);
            }

            SourceUnitPicker.SelectedIndex = 0; // Celsius
            TargetUnitPicker.SelectedIndex = 1; // Fahrenheit

            SourceUnitPicker.SelectedIndexChanged += OnUnitPickerChanged;
            TargetUnitPicker.SelectedIndexChanged += OnUnitPickerChanged;

            UpdateResult();
        }

        private void OnNumClicked(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                var digit = btn.Text;
                if (InputEntry.Text == "0")
                    InputEntry.Text = digit;
                else
                    InputEntry.Text += digit;

                UpdateResult();
            }
        }

        private void OnBackspaceClicked(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(InputEntry.Text) || InputEntry.Text == "0")
                return;

            if (InputEntry.Text.Length == 1)
                InputEntry.Text = "0";
            else
                InputEntry.Text = InputEntry.Text.Substring(0, InputEntry.Text.Length - 1);

            UpdateResult();
        }

        private void OnUnitPickerChanged(object? sender, EventArgs e)
        {
            UpdateResult();
        }

        private void UpdateResult()
        {
            if (!double.TryParse(InputEntry.Text, out double value))
                value = 0;

            var source = SourceUnitPicker.SelectedIndex >= 0 ? SourceUnitPicker.Items[SourceUnitPicker.SelectedIndex] : "Celsius";
            var target = TargetUnitPicker.SelectedIndex >= 0 ? TargetUnitPicker.Items[TargetUnitPicker.SelectedIndex] : "Fahrenheit";

            double converted = ConvertTemperature(value, source, target);

            ResultLabel.Text = converted.ToString();
        }

        private static double ConvertTemperature(double value, string from, string to)
        {
            if (from == to)
                return value;

            // Normalize to Celsius
            double celsius;
            switch (from)
            {
                case "Celsius":
                    celsius = value;
                    break;
                case "Fahrenheit":
                    celsius = (value - 32) * 5.0 / 9.0;
                    break;
                case "Kelvin":
                    celsius = value - 273.15;
                    break;
                default:
                    celsius = value;
                    break;
            }

            // Convert Celsius to target
            switch (to)
            {
                case "Celsius":
                    return celsius;
                case "Fahrenheit":
                    return celsius * 9.0 / 5.0 + 32;
                case "Kelvin":
                    return celsius + 273.15;
                default:
                    return celsius;
            }
        }

        private async void OnBackClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
