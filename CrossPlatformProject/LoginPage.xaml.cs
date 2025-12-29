using CrossPlatformProject.Services;

namespace CrossPlatformProject;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _auth;

    public LoginPage(AuthService auth)
    {
        InitializeComponent();
        _auth = auth;
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;

        var username = UsernameEntry.Text?.Trim() ?? "";
        var password = PasswordEntry.Text ?? "";

        var ok = await _auth.LoginAsync(username, password);
        if (!ok)
        {
            ErrorLabel.Text = "Invalid username or password";
            ErrorLabel.IsVisible = true;
            return;
        }

        // Go to main page and clear back stack
        await Shell.Current.GoToAsync("//MainPage");
    }
}
