using System;
using System.Linq;
using Microsoft.Maui.Controls;
using Converter.Services;
using Converter.Pages;

namespace Converter
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CategoriesView.ItemsSource = ConversionService.GetSampleCategories();
        }

        private async void CategoriesView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.CurrentSelection.FirstOrDefault();
            if (item == null)
                return;

            var category = (Models.UnitCategory)item;

            // Use Shell navigation if available, otherwise push onto Navigation
            if (Shell.Current != null)
                await Navigation.PushAsync(new CategoryPage(category));
            else
                await Navigation.PushAsync(new CategoryPage(category));

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
