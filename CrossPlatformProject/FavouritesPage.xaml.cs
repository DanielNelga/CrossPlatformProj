using CrossPlatformProject.Services;
namespace CrossPlatformProject;

public partial class FavouritesPage : ContentPage
{
    private IDispatcherTimer _clockTimer;

    public FavouritesPage()
	{
		InitializeComponent();
	}

    //Event handler to go back to mainPage

    private async void BackToMainPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");

    }

    //Event handler to go back to Settins page
    private async void Settings_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Setting");
    }

    protected override void OnAppearing()
    {

        base.OnAppearing();

        var favourites = FavouritesStore.Load();
        FavouritesList.ItemsSource = favourites;

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


}