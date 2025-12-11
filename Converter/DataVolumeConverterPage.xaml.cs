using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Converter
{
    public partial class DataVolumeConverterPage : ContentPage
    {
        // conversion to bytes
        readonly Dictionary<string, double> unitToBytes = new()
        {
            { "Bit", 1.0/8.0 },
            { "Byte", 1.0 },
            { "Kilobyte", 1024.0 },
            { "Megabyte", 1024.0 * 1024.0 },
            { "Gigabyte", 1024.0 * 1024.0 * 1024.0 }
        };

        public DataVolumeConverterPage()
        {
            InitializeComponent();

            foreach (var key in unitToBytes.Keys)
            {
                SourceUnitPicker.Items.Add(key);
                TargetUnitPicker.Items.Add(key);
            }

            SourceUnitPicker.SelectedIndex = 1; // Byte
            TargetUnitPicker.SelectedIndex = 2; // Kilobyte

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

            var source = SourceUnitPicker.SelectedIndex >= 0 ? SourceUnitPicker.Items[SourceUnitPicker.SelectedIndex] : "Byte";
            var target = TargetUnitPicker.SelectedIndex >= 0 ? TargetUnitPicker.Items[TargetUnitPicker.SelectedIndex] : "Kilobyte";

            if (!unitToBytes.TryGetValue(source, out double sourceFactor) || !unitToBytes.TryGetValue(target, out double targetFactor))
            {
                ResultLabel.Text = "Conversion error";
                return;
            }

            double valueInBytes = value * sourceFactor;
            double converted = valueInBytes / targetFactor;

            ResultLabel.Text = converted.ToString();
        }

        private async void OnBackClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}