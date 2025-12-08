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
        await Shell.Current.GoToAsync("..");

    }

    private async void Settings_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Settings));
    }
}