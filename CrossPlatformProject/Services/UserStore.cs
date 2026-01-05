using System.Text.Json;

namespace CrossPlatformProject.Services
{
    public static class UserStore
    {
        //path to users data stored in app's directory
        static string usersFile = Path.Combine(FileSystem.AppDataDirectory, "users.json");

        //represents signle user account
        public class User
        {
            public string Username { get; set; } = "";
            public string Password { get; set; } = "";
        }

        //loads people with account from local storage
        public static List<User> LoadUsers()
        {
            try
            {
                //if null or doesn't exit return empty list(not to crash)
                if (!File.Exists(usersFile))
                    return new List<User>();

                //read json
                string json = File.ReadAllText(usersFile);

                //deserialises json into a list of User objects
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch
            {
                //any errors return empty listr(not to crash)
                return new List<User>();
            }
        }

        //saves list of users to local storage
        public static void SaveUsers(List<User> users)
        {
            try
            {
                //serliase users list into json
                string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(usersFile, json);
            }
            catch(Exception)
            {

            }
        }

        //creates accounts
        public static bool CreateUser(string username, string password)
        {
            //load existing users
            var users = LoadUsers();

            // no duplicate usernames, and make them case sensitive
            foreach (var user in users)
            {
                if (user.Username.ToLower() == username.ToLower())
                {
                    return false;
                }
            }
            //add user and save list
            users.Add(new User { Username = username, Password = password });
            SaveUsers(users);

            return true;
        }

        public static bool ValidateLogin(string username, string password)
        {
            //loads users from local storage
            var users = LoadUsers();

            //checks all credentials match
            return users.Any(u =>
                u.Username.ToLower() == username.ToLower() &&
                u.Password == password);
        }
    }
}
