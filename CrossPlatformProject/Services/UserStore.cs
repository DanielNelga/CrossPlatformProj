using System.Text.Json;

namespace CrossPlatformProject.Services
{
    public static class UserStore
    {
        static string usersFile = Path.Combine(FileSystem.AppDataDirectory, "users.json");

        public class User
        {
            public string Username { get; set; } = "";
            public string Password { get; set; } = "";
        }

        public static List<User> LoadUsers()
        {
            try
            {
                if (!File.Exists(usersFile))
                    return new List<User>();

                string json = File.ReadAllText(usersFile);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch
            {
                return new List<User>();
            }
        }

        public static void SaveUsers(List<User> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(usersFile, json);
        }

        public static bool CreateUser(string username, string password)
        {
            var users = LoadUsers();

            // no duplicate usernames
            foreach (var user in users)
            {
                if (user.Username.ToLower() == username.ToLower())
                {
                    return false;
                }
            }

            users.Add(new User { Username = username, Password = password });
            SaveUsers(users);
            return true;
        }

        public static bool ValidateLogin(string username, string password)
        {
            var users = LoadUsers();

            return users.Any(u =>
                u.Username.ToLower() == username.ToLower() &&
                u.Password == password);
        }
    }
}
