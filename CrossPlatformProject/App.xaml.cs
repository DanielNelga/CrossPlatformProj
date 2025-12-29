using CrossPlatformProject.Services;
namespace CrossPlatformProject
{
    public partial class App : Application
    {
        private readonly AuthService _auth;
        public App(AuthService auth)
        {
            InitializeComponent();
            _auth = auth;

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

            if (!await _auth.IsLoggedInAsync())
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
        }

    }
}