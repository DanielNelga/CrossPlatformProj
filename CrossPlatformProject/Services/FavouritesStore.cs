using System.Text.Json;

namespace CrossPlatformProject.Services
{

    public static class FavouritesStore
    {

        static string FilePath()
        {
            string user = Preferences.Get("LoggedInUser", "guest");
            return Path.Combine(FileSystem.AppDataDirectory, $"favourites_{user}.json");

        }
        public static List<Movie> Load()
        {
            string file = FilePath();

            if (File.Exists(file) == false)
            {
                return new List<Movie>();
            }

            try
            {
                string json = File.ReadAllText(file);
                List<Movie> movies = JsonSerializer.Deserialize<List<Movie>>(json);

                if (movies == null)
                {
                    return new List<Movie>();
                }

                return movies;
            }
            catch
            {
                return new List<Movie>();
            }
        }

        public static void Save(List<Movie> movies)
        {
            string file = FilePath();

            string json = JsonSerializer.Serialize(movies, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(file, json);
        }

        public static void Add(Movie movie)
        {
            List<Movie> favourites = Load();

            foreach (Movie m in favourites)
            {
                if (m.Title == movie.Title)
                {
                    return;
                }
            }

            favourites.Add(movie);
            Save(favourites);
        }
    }
}
