using CrossPlatformProject.Services;

namespace CrossPlatformProject
{
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        //create account event handler
        private async void Create_Clicked(object sender, EventArgs e)
        {
            //make error button false
            ErrorLabel.IsVisible = false;

            //get the user inputs
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            string confirm = ConfirmEntry.Text;

            //check that fields have been filled in
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ErrorLabel.Text = "Fill in all fields.";
                ErrorLabel.IsVisible = true;
                return;
            }

            //make sure they meet the minimum password requirements
            if (password.Length < 8)
            {
                ErrorLabel.Text = "Password must be at least 8 characters long.";
                ErrorLabel.IsVisible = true;
                return;
            }

            //password and confirmation password have to match
            if (password != confirm)
            {
                ErrorLabel.Text = "Passwords do not match.";
                ErrorLabel.IsVisible = true;
                return;
            }

            
            //create the account
            bool userCreated = UserStore.CreateUser(username, password);

            //if account exists show this message 
            if (userCreated == false)
            {
                ErrorLabel.Text = "That username already exists.";
                ErrorLabel.IsVisible = true;
                return;
            }

            await DisplayAlert("Success", "Account created!", "OK");

            await Shell.Current.GoToAsync("..");
        }

        //go back a paghe( to the login page)
        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
