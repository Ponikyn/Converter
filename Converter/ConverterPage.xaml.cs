using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Converter
{
    public partial class ConverterPage : ContentPage
    {
        readonly Dictionary<string, double> unitToMeters = new()
        {
            { "Millimeter", 0.001 },
            { "Centimeter", 0.01 },
            { "Meter", 1.0 },
            { "Kilometer", 1000.0 },
            { "Mile", 1609.344 }
        };

        public ConverterPage()
        {
            InitializeComponent();

            // Populate unit pickers
            foreach (var key in unitToMeters.Keys)
            {
                SourceUnitPicker.Items.Add(key);
                TargetUnitPicker.Items.Add(key);
            }

            SourceUnitPicker.SelectedIndex = 2; // Meter
            TargetUnitPicker.SelectedIndex = 0; // Millimeter

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

            var source = SourceUnitPicker.SelectedIndex >= 0 ? SourceUnitPicker.Items[SourceUnitPicker.SelectedIndex] : "Centimeter";
            var target = TargetUnitPicker.SelectedIndex >= 0 ? TargetUnitPicker.Items[TargetUnitPicker.SelectedIndex] : "Meter";

            if (!unitToMeters.TryGetValue(source, out double sourceFactor) || !unitToMeters.TryGetValue(target, out double targetFactor))
            {
                ResultLabel.Text = "Conversion error";
                return;
            }

            // Convert source value to meters, then to target
            double valueInMeters = value * sourceFactor;
            double converted = valueInMeters / targetFactor;

            ResultLabel.Text = converted.ToString();
        }

        private async void OnBackClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}