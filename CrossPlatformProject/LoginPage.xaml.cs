using CrossPlatformProject.Services;

namespace CrossPlatformProject;

public partial class LoginPage : ContentPage
{
    

    public LoginPage()
    {
        InitializeComponent();
   
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;

        var username = UsernameEntry.Text?.Trim() ?? "";
        var password = PasswordEntry.Text ?? "";

        if(password.Length <8)
        {
            ErrorLabel.Text = "Password must be at least 8 characters long.";
            ErrorLabel.IsVisible = true;
            return;
        }

        bool ok = UserStore.ValidateLogin(username, password);



        if (!ok)
        {
            ErrorLabel.Text = "Invalid username or password";
            ErrorLabel.IsVisible = true;
            return;
        }

        Preferences.Set("LoggedInUser", username);
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void Signup_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignupPage));
    }
}

