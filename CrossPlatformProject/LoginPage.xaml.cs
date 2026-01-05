using CrossPlatformProject.Services;

namespace CrossPlatformProject;

public partial class LoginPage : ContentPage
{
    
    //constructor
    public LoginPage()
    {
        InitializeComponent();
   
    }

    //login button event handler
    private async void Login_Clicked(object sender, EventArgs e)
    {
        //hide error label
        ErrorLabel.IsVisible = false;

        //get the input values from the user
        var username = UsernameEntry.Text?.Trim() ?? "";
        var password = PasswordEntry.Text ?? "";

        //minimum length requirement
        if(password.Length <8)
        {
            ErrorLabel.Text = "Password must be at least 8 characters long.";
            ErrorLabel.IsVisible = true;
            return;
        }

        //validate credentials with user accounts
        bool ok = UserStore.ValidateLogin(username, password);


        //if invalid show this error
        if (!ok)
        {
            ErrorLabel.Text = "Invalid username or password";
            ErrorLabel.IsVisible = true;
            return;
        }
        //save the logged in username
        Preferences.Set("LoggedInUser", username);

        //nav to mainpage after login
        await Shell.Current.GoToAsync("//MainPage");
    }
    //navigate to the sign up page
    private async void Signup_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignupPage));
    }
}

