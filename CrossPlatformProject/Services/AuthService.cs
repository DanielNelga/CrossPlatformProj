
namespace CrossPlatformProject.Services
{
    public class AuthService
    {
        private const string TokenKey = "auth_token";

        public async Task<bool> IsLoggedInAsync()
        {
            var token = await SecureStorage.GetAsync(TokenKey);
            return !string.IsNullOrWhiteSpace(token);
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            // Demo credentials (change later)
            if (username == "admin" && password == "1234")
            {
                await SecureStorage.SetAsync(TokenKey, "demo-token");
                return true;
            }

            return false;
        }

        public void Logout()
        {
            SecureStorage.Remove(TokenKey);
        }
    }
}
