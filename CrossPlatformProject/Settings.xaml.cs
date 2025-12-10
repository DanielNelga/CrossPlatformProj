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
        //goes straight to mainpage
        await Shell.Current.GoToAsync("//MainPage");

    }

    private async void Settings_Clicked(object sender, EventArgs e)
    {
        //goes straight to settings
        await Shell.Current.GoToAsync("//Settings");
    }

    private async void Favourites_Clicked(object sender, EventArgs e)
    {
        //goes straight to favourites as the // skips all the previous pages
        await Shell.Current.GoToAsync("//FavouritesPage");
    }
}