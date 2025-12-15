namespace CrossPlatformProject;

public partial class FavouritesPage : ContentPage
{
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

   
}