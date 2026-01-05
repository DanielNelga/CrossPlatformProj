using CrossPlatformProject.Services;
namespace CrossPlatformProject
{
    public partial class App : Application
    {
        private readonly AuthService _auth;
        static readonly Color Navy = Color.FromArgb("#0F172A");
        static readonly Color Champagne = Color.FromArgb("#E6D3A3");
        static readonly Color OnChampagne = Colors.Black;

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
                // Dark theme (cinematic)
                Resources["BgColor"] = Navy;
                Resources["TitleColor"] = Champagne;
                Resources["SubTextColor"] = Champagne;

                Resources["EntryBg"] = Champagne;
                Resources["EntryText"] = OnChampagne;
                Resources["EntryPlaceholder"] = OnChampagne;

                Resources["ButtonBg"] = Champagne;
                Resources["ButtonText"] = OnChampagne;
            }
            else
            {
                // Light theme (clean)
                Resources["BgColor"] = Champagne;
                Resources["TitleColor"] = Navy;
                Resources["SubTextColor"] = Navy;

                Resources["EntryBg"] = Navy;
                Resources["EntryText"] = Navy;
                Resources["EntryPlaceholder"] = Navy;

                Resources["ButtonBg"] = Navy;
                Resources["ButtonText"] = Navy;
            }

        }


    }
}