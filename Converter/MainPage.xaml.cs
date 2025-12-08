namespace Converter
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSquareButtonClicked(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                DisplayAlert("Button", $"You pressed {btn.Text}", "OK");
            }
        }
    }
}
