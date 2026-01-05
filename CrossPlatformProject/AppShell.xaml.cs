namespace CrossPlatformProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //navigation betwen pages
            //historyPage route
            Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));

            //signupPage route
            Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));

            //loginPage route
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

            //settings route
            Routing.RegisterRoute("Setting", typeof(SettingsPage));

            //favourites route
            Routing.RegisterRoute("FavouritesPage", typeof(FavouritesPage));

            //movieDetailsPage route
            Routing.RegisterRoute(nameof(MovieDetailPage), typeof(MovieDetailPage));

        }

        
    }
}
