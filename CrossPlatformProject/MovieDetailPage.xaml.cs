using System.Collections.Generic;
namespace CrossPlatformProject;


[QueryProperty(nameof(Movie), "Movie")]
public partial class MovieDetailPage : ContentPage
{
	
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
		}
    }
	//home page button when you click on the button in the movieDetailPage
	private async void BackToMainPage_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}


    private async void Settings_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Settings));
    }

    private async void Favourites_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FavouritesPage));
    }
}