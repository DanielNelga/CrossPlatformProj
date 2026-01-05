

namespace CrossPlatformProject;

public partial class SettingsPage : ContentPage
{
    //stores user settings 
    private SettingsList MovieSettings;

    //timer used for clock
    private IDispatcherTimer _clockTimer;

    private bool _isInitializing;


    public SettingsPage()
	{
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            _isInitializing = true;

            // Fade in
            if (PageContainer != null)
            {
                PageContainer.Opacity = 0;
                await Task.Delay(120);
                await PageContainer.FadeTo(1, 1150, Easing.CubicInOut);
            }

            // Load settings once
            MovieSettings = ManageSettings.Load() ?? new SettingsList();

            // Apply UI values safely
            if (DarkModeSwitch != null)
                DarkModeSwitch.IsToggled = MovieSettings.DarkMode;

            if (ClockSwitch != null)
                ClockSwitch.IsToggled = MovieSettings.ShowClock;

            if (clockLabel != null)
                clockLabel.IsVisible = MovieSettings.ShowClock;

            EnsureClockTimer();
            UpdateClockRunningState(MovieSettings.ShowClock);

            _isInitializing = false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"SettingsPage OnAppearing crash: {ex}");
            await DisplayAlert("Error", $"Settings page failed to load:\n{ex.Message}", "OK");
        }
    }



    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _clockTimer?.Stop();
    }

    //displaying the live clock
    private void EnsureClockTimer()
    {
        if (_clockTimer != null) 
            return;

        _clockTimer = Dispatcher.CreateTimer();
        _clockTimer.Interval = TimeSpan.FromSeconds(1);

        //fire event every sec while running
        _clockTimer.Tick += (_, __) =>
        {
            //only update if it exists + visible
            if (clockLabel != null && clockLabel.IsVisible)
                clockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        };
    }

    private void UpdateClockRunningState(bool shouldRun)
    {
        if (_clockTimer == null) 
            return;

        if (shouldRun)
        {
            //update immediately once
            if (clockLabel != null)
                clockLabel.Text = DateTime.Now.ToString("HH:mm:ss");

            _clockTimer.Start();
        }
        else
        {
            _clockTimer.Stop();
        }
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
        await Shell.Current.GoToAsync(nameof(FavouritesPage));
    }

    private void DarkMode_Toggled(object sender, ToggledEventArgs e)
    {

        if (_isInitializing)
            return;


        //update and save the dark mode settings
        MovieSettings.DarkMode = e.Value;
        ManageSettings.Save(MovieSettings);

        //apply theme instantly
        ((App)Application.Current).ApplyTheme(e.Value);

        Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
    }

    private async void Logout_Clicked(object sender, EventArgs e)
    {
        //confirm logout
            bool confirm = await DisplayAlert(
                "Logout",
                "Do you really want to logout?",
                "Yes",
                "No"
                
            );

            if (!confirm)
                return;
        
            //remove stored logout info(token)
        Preferences.Remove("LoggedInUser");

        //nav to login page
        await Shell.Current.GoToAsync("LoginPage");

    }

    //go to history page
    private async void History_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HistoryPage));
    }

    //clear all favourites
    private async void ClearAllFavourites_Clicked(object sender, EventArgs e)
    {

        //displayalert confirm the clear
        bool confirm = await DisplayAlert(
                "Clear All Favourites",
                "Do you really want to clear your favourites?",
                "Yes",
                "No"
            );

        if (confirm == false)
            return;


        //clear from storage
        CrossPlatformProject.Services.FavouritesStore.Clear();
        
    }

    private void Clock_Toggled(object sender, ToggledEventArgs e)
    {

        if (_isInitializing)
            return;


        //update clock visability 
        MovieSettings.ShowClock = e.Value;
        ManageSettings.Save(MovieSettings);
    }



}