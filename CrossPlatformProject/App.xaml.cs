namespace CrossPlatformProject
{
    public partial class App : Application
    {
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


    }
}