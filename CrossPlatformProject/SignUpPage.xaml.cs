using CrossPlatformProject.Services;

namespace CrossPlatformProject
{
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private async void Create_Clicked(object sender, EventArgs e)
        {
            ErrorLabel.IsVisible = false;

            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            string confirm = ConfirmEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ErrorLabel.Text = "Fill in all fields.";
                ErrorLabel.IsVisible = true;
                return;
            }

            if (password.Length < 8)
            {
                ErrorLabel.Text = "Password must be at least 8 characters long.";
                ErrorLabel.IsVisible = true;
                return;
            }


            if (password != confirm)
            {
                ErrorLabel.Text = "Passwords do not match.";
                ErrorLabel.IsVisible = true;
                return;
            }

            

            bool userCreated = UserStore.CreateUser(username, password);

            if (userCreated == false)
            {
                ErrorLabel.Text = "That username already exists.";
                ErrorLabel.IsVisible = true;
                return;
            }

            await DisplayAlert("Success", "Account created!", "OK");

            await Shell.Current.GoToAsync("..");
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
