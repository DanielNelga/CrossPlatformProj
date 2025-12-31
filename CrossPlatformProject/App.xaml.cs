using CrossPlatformProject.Services;
namespace CrossPlatformProject
{
    public partial class App : Application
    {
        private readonly AuthService _auth;
        public App()
        {
            InitializeComponent();
       

            try
            {
                var settings = ManageSettings.Load();
                UserAppTheme = settings.DarkMode ? AppTheme.Dark : AppTheme.Light;
            }
            catch (Exception ex)
            {
                UserAppTheme = AppTheme.Light;
            }

            MainPage = new AppShell();

        }
        protected override async void OnStart()
        {
            base.OnStart();

            if(!Preferences.ContainsKey("LoggedInUser"))
            {
                Shell.Current.GoToAsync(nameof(LoginPage));
            }
        }

    }
}