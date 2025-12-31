
namespace CrossPlatformProject;

public partial class SettingsPage : ContentPage
{
    private SettingsList MovieSettings;
    private IDispatcherTimer _clockTimer;
	public SettingsPage()
	{
        InitializeComponent();
	}

    protected override void OnAppearing()
    {
        
        base.OnAppearing();
        try
        {
            MovieSettings = ManageSettings.Load() ?? new SettingsList();

            if (DarkModeSwitch != null)
            {
                DarkModeSwitch.IsToggled = MovieSettings.DarkMode;
            }
            DarkModeSwitch.IsToggled = MovieSettings.DarkMode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }

        AppClock();

        _clockTimer = Dispatcher.CreateTimer();
        _clockTimer.Interval = TimeSpan.FromSeconds(1);
        _clockTimer.Tick += (s, e) => AppClock();
        _clockTimer.Start();


    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _clockTimer?.Stop();
    }

    //Displaying the live clock
    private void AppClock()
    {
        clockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
    }

    //home page button when you click on the button in the movieDetailPage
    private async void BackToMainPage_Clicked(object sender, EventArgs e)
    {
        //goes straight to mainpage
        await Shell.Current.GoToAsync("//MainPage");

    }

    

    private async void Favourites_Clicked(object sender, EventArgs e)
    {
        //goes straight to favourites as the // skips all the previous pages
        await Shell.Current.GoToAsync("FavouritesPage");
    }

    private void DarkMode_Toggled(object sender, ToggledEventArgs e)
    {
        MovieSettings.DarkMode = e.Value;
        ManageSettings.Save(MovieSettings);

        Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
    }

    private async void Logout_Clicked(object sender, EventArgs e)
    {
        Preferences.Remove("LoggedInUser");

        await Shell.Current.GoToAsync("LoginPage");
    }

}