using CrossPlatformProject.Services;
namespace CrossPlatformProject
{
    public partial class App : Application
    {
        //authentication service
        private readonly AuthService _auth;

        //theme colours
        static readonly Color Navy = Color.FromArgb("#0F172A");
        static readonly Color Champagne = Color.FromArgb("#E6D3A3");
        static readonly Color OnChampagne = Colors.Black;

        public App()
        {
            InitializeComponent();
       

            try
            {
                //load saved settings
                var settings = ManageSettings.Load();

                //apply preferrred theme
                ApplyTheme(settings.DarkMode);

                //tells maui which is currently active
                UserAppTheme = settings.DarkMode ? AppTheme.Dark : AppTheme.Light;
            }

            catch (Exception ex)
            {
                //load light mode if all fails
                UserAppTheme = AppTheme.Light;
            }

            //root nav container
            MainPage = new AppShell();

        }

        protected override async void OnStart()
        {
            base.OnStart();

            //goes to login page if not previously logged in
            if(!Preferences.ContainsKey("LoggedInUser"))
            {
                Shell.Current.GoToAsync(nameof(LoginPage));
            }
        }


        public void ApplyTheme(bool dark)
        {
            if (dark)
            {
               //dark theme colours
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
                //light theme colours
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