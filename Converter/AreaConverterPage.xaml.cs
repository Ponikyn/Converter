using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Converter
{
    public partial class AreaConverterPage : ContentPage
    {
        // area units conversion to square meters
        readonly Dictionary<string, double> unitToSquareMeters = new()
        {
            { "Square millimeter", 1e-6 },
            { "Square centimeter", 1e-4 },
            { "Square meter", 1.0 },
            { "Square kilometer", 1e6 },
            { "Hectare", 10000.0 },
            { "Acre", 4046.8564224 }
        };

        public AreaConverterPage()
        {
            InitializeComponent();

            foreach (var key in unitToSquareMeters.Keys)
            {
                SourceUnitPicker.Items.Add(key);
                TargetUnitPicker.Items.Add(key);
            }

            SourceUnitPicker.SelectedIndex = 2; // Square meter
            TargetUnitPicker.SelectedIndex = 1; // Square centimeter

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

            var source = SourceUnitPicker.SelectedIndex >= 0 ? SourceUnitPicker.Items[SourceUnitPicker.SelectedIndex] : "Square centimeter";
            var target = TargetUnitPicker.SelectedIndex >= 0 ? TargetUnitPicker.Items[TargetUnitPicker.SelectedIndex] : "Square meter";

            if (!unitToSquareMeters.TryGetValue(source, out double sourceFactor) || !unitToSquareMeters.TryGetValue(target, out double targetFactor))
            {
                ResultLabel.Text = "Conversion error";
                return;
            }

            double valueInSqM = value * sourceFactor;
            double converted = valueInSqM / targetFactor;

            ResultLabel.Text = converted.ToString();
        }

        private async void OnBackClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}