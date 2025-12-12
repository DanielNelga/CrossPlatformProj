using System.Diagnostics;

namespace CrossPlatformProject;

public partial class SettingsPage : ContentPage
{
    private SettingsList MovieSettings;
	public SettingsPage()
	{
        InitializeComponent();
	}

    protected override void OnAppearing()
    {
        
        base.OnAppearing();
        try
        {
            MovieSettings = ManageSettings.Load() ?? new SettingsList();

            if (DarkModeSwitch != null)
            {
                DarkModeSwitch.IsToggled = MovieSettings.DarkMode;
            }
            DarkModeSwitch.IsToggled = MovieSettings.DarkMode;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
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
        await Shell.Current.GoToAsync("FavouritesPage");
    }

    private void DarkMode_Toggled(object sender, ToggledEventArgs e)
    {
        MovieSettings.DarkMode = e.Value;
        ManageSettings.Save(MovieSettings);

        Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
    }
}