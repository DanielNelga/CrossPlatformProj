using CrossPlatformProject.Services;
namespace CrossPlatformProject;


[QueryProperty(nameof(Movie), "Movie")]

public partial class MovieDetailPage : ContentPage
{

    private IDispatcherTimer _clockTimer;


    public Movie Movie { get; set; }
	public MovieDetailPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
		base.OnAppearing();
		if (Movie != null)
		{
			//display the genres in the movieDetailPage when user clicked on the movie in search list
			Movie.GenreDisplay = string.Join(",", Movie.Genre);
			BindingContext = Movie;

            AppClock();

            _clockTimer = Dispatcher.CreateTimer();
            _clockTimer.Interval = TimeSpan.FromSeconds(1);
            _clockTimer.Tick += (s, e) => AppClock();
            _clockTimer.Start();
        }
    }


	//home page button when you click on the button in the movieDetailPage
	private async void BackToMainPage_Clicked(object sender, EventArgs e)
	{
        //goes straight to mainpage as the main page

        await Shell.Current.GoToAsync("//MainPage");
    }


    private async void Settings_Clicked(object sender, EventArgs e)
    {
        //goes straight to settings as the // skips all the previous pages

        await Shell.Current.GoToAsync("Setting");
    }

    private async void Favourites_Clicked(object sender, EventArgs e)
    {
        //goes straight to favourites as the // skips all the previous pages

        await Shell.Current.GoToAsync("FavouritesPage");
    }


    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _clockTimer?.Stop();
    }

    private void AppClock()
    {
        clockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
    }

    private async void AddToFavourites_Clicked(object sender, EventArgs e)
    {
        FavouritesStore.Add(Movie);
        HistoryStore.Add(Movie, "Favourited");
        await DisplayAlert("Success ", "Movie added to favourites", "OK");
    }
}