using System.Text.Json;

namespace CrossPlatformProject.Services
{

    public static class FavouritesStore
    {
        //builds path, each user gets their own files
        static string FilePath()
        {
            //retrieve the logged-in user(defaults to guest)
            string user = Preferences.Get("LoggedInUser", "guest");

            //filename will be unique to every user
            return Path.Combine(FileSystem.AppDataDirectory, $"favourites_{user}.json");

        }

        //loads favourite movies fromn local storage
        public static List<Movie> Load()
        {
            string file = FilePath();

            //if not present return null(not to crahs)
            if (File.Exists(file) == false)
            {
                return new List<Movie>();
            }

            try
            {
                //Read file
                string json = File.ReadAllText(file);

                //deserialise JSON into a list of movie objects
                List<Movie> movies = JsonSerializer.Deserialize<List<Movie>>(json);

                //if deserialisation fails or returns null, return an empty list(not to crash)
                if (movies == null)
                {
                    return new List<Movie>();
                }

                //loaded favourites
                return movies;
            }
            catch
            {
                //return empty list incase of erros
                return new List<Movie>();
            }
        }

        //saves the list of Favourite movies to local storage
        public static void Save(List<Movie> movies)
        {
            try
            {

                string file = FilePath();

                //serialise movie list into Json
                string json = JsonSerializer.Serialize(movies, new JsonSerializerOptions
                {
                    //readable to us
                    WriteIndented = true
                });

                //wirte Json data to a file
                File.WriteAllText(file, json);
            }
            catch(Exception) 
            {
                
            }
        }

        //adds movie to favourites
        public static void Add(Movie movie)
        {
            //load favourites
            List<Movie> favourites = Load();

            //prevents duplication
            foreach (Movie m in favourites)
            {
                if (m.Title == movie.Title)
                {
                    return;
                }
            }

            //add movie to favourites
            favourites.Add(movie);
            Save(favourites);
        }

        //remove movie from favourites
        public static void Remove(Movie movie)
        {
            //load favourites
            List<Movie> favourites = Load();

            //Removes matching movie by the title
            for (int i = 0; i < favourites.Count; i++)
            {
                if (favourites[i].Title == movie.Title)
                {
                    favourites.RemoveAt(i);
                    break;
                }
            }

            //saves new list
            Save(favourites);
        }

        //Clears favourites list
        public static void Clear()
        {
            Save(new List<Movie>());
        }

    }
}
