namespace CrossPlatformProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //navigation betwen pages
            Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute("Setting", typeof(SettingsPage));
            Routing.RegisterRoute("FavouritesPage", typeof(FavouritesPage));
            Routing.RegisterRoute(nameof(MovieDetailPage), typeof(MovieDetailPage));

        }

        
    }
}
