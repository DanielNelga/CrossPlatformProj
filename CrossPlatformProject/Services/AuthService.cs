
namespace CrossPlatformProject.Services
{
    //handles user authentication
    public class AuthService
    {

        //key to store and recieve authentication token
        private const string TokenKey = "auth_token";

        //Checks if logged in, returns true is token is valid
        public async Task<bool> IsLoggedInAsync()
        {
            //Receive the token
            var token = await SecureStorage.GetAsync(TokenKey);

            //user loggin if token valid
            return !string.IsNullOrWhiteSpace(token);
        }


        //Logs out the user by removing token
        public void Logout()
        {
            SecureStorage.Remove(TokenKey);
        }
    }
}
