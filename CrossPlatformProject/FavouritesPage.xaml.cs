using CrossPlatformProject.Services;
namespace CrossPlatformProject;

public partial class FavouritesPage : ContentPage
{
    //for timer
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
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }

    protected override async void OnAppearing()
    {

        base.OnAppearing();

        //load favourites
        var favourites = FavouritesStore.Load();

        //bind favourites to listview
        FavouritesList.ItemsSource = favourites;

        //for fade animation
        FavouritesList.Opacity = 0;
        //delay before starting 
        await Task.Delay(120);
        await FavouritesList.FadeTo(1, 1150, Easing.CubicInOut);


        AppClock();

        //load settings 
        var settings = ManageSettings.Load() ?? new SettingsList();

        //show clock
        clockLabel.IsVisible = settings.ShowClock;


        _clockTimer = Dispatcher.CreateTimer();
        _clockTimer.Interval = TimeSpan.FromSeconds(1);
        _clockTimer.Tick += (s, e) => AppClock();
        _clockTimer.Start();


    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        //stop clock
        _clockTimer?.Stop();
    }

    //Displaying the live clock
    private void AppClock()
    {
        clockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
    }

    private async void Remove_Clicked(object sender, EventArgs e)
    {
        //getting button that was clicked
        Button btn = sender as Button;

        //retrieve movie object via CommandParameter
        Movie movie = btn?.CommandParameter as Movie;

        if (movie == null)
            return;

        //better ui, ask for confirmation
        bool confirm = await DisplayAlert(
            "Remove Favourite",
            "Are you sure that you want to remove from favourites?",
            "Yes",
            "No");

        //cancel if user presses no
        if (confirm == false)
            return;

        //remove from favourites
        FavouritesStore.Remove(movie);

        //refresh the list
        FavouritesList.ItemsSource = FavouritesStore.Load();

        //let user know its been removed, good UI/UX
        await DisplayAlert("Removed", "Movie removed from favourites", "OK");
    }



}