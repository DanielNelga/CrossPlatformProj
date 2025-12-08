namespace CrossPlatformProject;

public partial class FavouritesPage : ContentPage
{
	public FavouritesPage()
	{
		InitializeComponent();
	}

    private async void BackToMainPage_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");

    }

    private async void Settings_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Settings");
    }

    private async void Favourites_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//FavouritesPage");
    }
}