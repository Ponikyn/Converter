namespace Converter
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register route for MenuPage so Shell navigation works
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
            // Register route for ConverterPage
            Routing.RegisterRoute(nameof(ConverterPage), typeof(ConverterPage));
            // Register route for MassConverterPage
            Routing.RegisterRoute(nameof(MassConverterPage), typeof(MassConverterPage));
        }
    }
}
