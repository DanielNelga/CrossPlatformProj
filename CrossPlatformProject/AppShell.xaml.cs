namespace CrossPlatformProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //navigation betwen pages
            Routing.RegisterRoute(nameof(MovieDetailPage), typeof(MovieDetailPage));
            //settings page navigation by pressing the button on bottom nav bar
            Routing.RegisterRoute(nameof(Settings), typeof(Settings));
        }

        
    }
}
