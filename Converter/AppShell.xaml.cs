namespace Converter
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register route for MenuPage so Shell navigation works
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
        }
    }
}
