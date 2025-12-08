namespace CrossPlatformProject;

public partial class Settings : ContentPage
{
	public Settings()
	{
        InitializeComponent();
	}

    //home page button when you click on the button in the movieDetailPage
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