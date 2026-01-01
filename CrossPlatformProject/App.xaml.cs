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
                ApplyTheme(settings.DarkMode);
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

        public void ApplyTheme(bool dark)
        {
            if (dark)
            {
                Resources["BgColor"] = Colors.Black;
                Resources["TitleColor"] = Colors.Gold;
                Resources["SubTextColor"] = Colors.Gold;

                Resources["EntryBg"] = Colors.Gold;
                Resources["EntryText"] = Colors.Black;
                Resources["EntryPlaceholder"] = Colors.Black;

                Resources["ButtonBg"] = Colors.Gold;
                Resources["ButtonText"] = Colors.Black;
            }
            else
            {
                Resources["BgColor"] = Colors.White;
                Resources["TitleColor"] = Colors.Black;
                Resources["SubTextColor"] = Colors.Gray;

                Resources["EntryBg"] = Colors.White;
                Resources["EntryText"] = Colors.Black;
                Resources["EntryPlaceholder"] = Colors.Gray;

                Resources["ButtonBg"] = Colors.Black;
                Resources["ButtonText"] = Colors.White;
            }
        }


    }
}