using System;
using System.Linq;
using Microsoft.Maui.Controls;
using Converter.Models;
using Converter.Services;
using System.Collections.Generic;

namespace Converter.Pages
{
    public partial class CategoryPage : ContentPage
    {
        UnitCategory _category;
        public CategoryPage(UnitCategory category)
        {
            InitializeComponent();
            _category = category;
            CategoryTitle.Text = category.Name;

            FromPicker.ItemsSource = category.Units;
            FromPicker.ItemDisplayBinding = new Binding("Name");
            ToPicker.ItemsSource = category.Units;
            ToPicker.ItemDisplayBinding = new Binding("Name");

            FromPicker.SelectedIndex = 0;
            ToPicker.SelectedIndex = 1;

            BuildNumericPad();
        }

        void BuildNumericPad()
        {
            // Ensure previous definitions/children are removed to avoid duplicates on rebuild
            PadGrid.Children.Clear();
            PadGrid.RowDefinitions.Clear();
            PadGrid.ColumnDefinitions.Clear();

            // 3 columns x 4 rows keypad with equal sizing
            for (int r = 0; r < 4; r++)
                PadGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            for (int c = 0; c < 3; c++)
                PadGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            var buttons = new List<string>
            {
                "7","8","9",
                "4","5","6",
                "1","2","3",
                ".","0","?"
            };

            int idx = 0;
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    var t = buttons[idx++];
                    var btn = new Button
                    {
                        Text = t,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Fill,
                        FontSize = 20,
                        Padding = new Thickness(0),
                        Margin = new Thickness(4)
                    };
                    btn.Clicked += KeypadClicked;

                    PadGrid.Children.Add(btn);
                    Grid.SetRow(btn, r);
                    Grid.SetColumn(btn, c);
                }
            }
        }

        void KeypadClicked(object sender, EventArgs e)
        {
            if (InputEntry == null)
                return;

            var b = (Button)sender;
            var t = b.Text;
            if (t == "?")
            {
                if (!string.IsNullOrEmpty(InputEntry.Text))
                    InputEntry.Text = InputEntry.Text.Substring(0, InputEntry.Text.Length - 1);
            }
            else if (t == ".")
            {
                if (string.IsNullOrEmpty(InputEntry.Text))
                    InputEntry.Text = "0.";
                else if (!InputEntry.Text.Contains("."))
                    InputEntry.Text += ".";
            }
            else
            {
                InputEntry.Text = (InputEntry.Text ?? "") + t;
            }

            Recalculate();
        }

        void SwapClicked(object sender, EventArgs e)
        {
            var fi = FromPicker.SelectedIndex;
            FromPicker.SelectedIndex = ToPicker.SelectedIndex;
            ToPicker.SelectedIndex = fi;
            Recalculate();
        }

        void ClearClicked(object sender, EventArgs e)
        {
            InputEntry.Text = string.Empty;
            ResultLabel.Text = string.Empty;
        }

        void Recalculate()
        {
            if (FromPicker.SelectedItem == null || ToPicker.SelectedItem == null)
                return;

            var from = (UnitItem)FromPicker.SelectedItem;
            var to = (UnitItem)ToPicker.SelectedItem;

            if (string.IsNullOrEmpty(InputEntry.Text))
            {
                ResultLabel.Text = string.Empty;
                return;
            }

            if (!double.TryParse(InputEntry.Text, out double input))
            {
                ResultLabel.Text = "Неверный ввод";
                return;
            }

            // handle temperature specially
            if (_category.Id == "temperature")
            {
                if (!ConversionService.TryConvertTemperature(from.Id, to.Id, input, out double res))
                {
                    ResultLabel.Text = "Операция невозможна";
                }
                else
                {
                    ResultLabel.Text = res.ToString("G6");
                }
                return;
            }

            try
            {
                var result = ConversionService.Convert(from, to, input);
                if (double.IsInfinity(result) || double.IsNaN(result))
                {
                    ResultLabel.Text = "Невозможно вычислить";
                }
                else
                {
                    ResultLabel.Text = result.ToString("G6");
                }
            }
            catch (Exception ex)
            {
                ResultLabel.Text = "Ошибка: " + ex.Message;
            }
        }
    }
}