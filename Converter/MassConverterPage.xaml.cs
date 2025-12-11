using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Converter
{
    public partial class MassConverterPage : ContentPage
    {
        readonly Dictionary<string, double> unitToKilogram = new()
        {
            { "Milligram", 1e-6 },
            { "Gram", 1e-3 },
            { "Kilogram", 1.0 },
            { "Tonne", 1000.0 },
            { "Pound", 0.45359237 }
        };

        public MassConverterPage()
        {
            InitializeComponent();

            foreach (var key in unitToKilogram.Keys)
            {
                SourceUnitPicker.Items.Add(key);
                TargetUnitPicker.Items.Add(key);
            }

            SourceUnitPicker.SelectedIndex = 2; // Kilogram
            TargetUnitPicker.SelectedIndex = 1; // Gram

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

            var source = SourceUnitPicker.SelectedIndex >= 0 ? SourceUnitPicker.Items[SourceUnitPicker.SelectedIndex] : "Gram";
            var target = TargetUnitPicker.SelectedIndex >= 0 ? TargetUnitPicker.Items[TargetUnitPicker.SelectedIndex] : "Kilogram";

            if (!unitToKilogram.TryGetValue(source, out double sourceFactor) || !unitToKilogram.TryGetValue(target, out double targetFactor))
            {
                ResultLabel.Text = "Conversion error";
                return;
            }

            double valueInKg = value * sourceFactor;
            double converted = valueInKg / targetFactor;

            ResultLabel.Text = converted.ToString();
        }

        private async void OnBackClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}