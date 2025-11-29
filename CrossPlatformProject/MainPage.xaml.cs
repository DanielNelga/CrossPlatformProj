using System.Text.Json;
namespace CrossPlatformProject
{
    //MainPage
    public partial class MainPage : ContentPage
    {
        //using the json API can getting movies+movie objects from here
        string jsonFileGithub = "https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json";

        //storing in local cache here
        string localCache = Path.Combine(FileSystem.AppDataDirectory, "movies.json");

        //creating the list and using the external Movie class
        List<Movie> allMovies = new List<Movie>();

        public MainPage()
        {
            InitializeComponent();
            LoadMovies();
        }

        private async void LoadMovies()
        {
            try
            {
                string jsonFile;
                //going to create a case, if the program doesn't find the file instead of crashing
                if (!File.Exists(localCache))
                {
                    //downloads file if it doesn't exist or can't find it it will download it from github and use the string jsonFile to store
                    HttpClient client = new HttpClient();


                    jsonFile = await client.GetStringAsync(jsonFileGithub);
                    File.WriteAllText(localCache, jsonFile);

                }
                else
                {
                    jsonFile = File.ReadAllText(localCache);
                }
                allMovies = JsonSerializer.Deserialize<List<Movie>>(jsonFile);
                MoviesList.ItemsSource = allMovies;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error, issue detected ", ex.Message, "OK");

            }

        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            //making search not case sensitive by using .ToLower();
            string searchText = e.NewTextValue.ToLower();
            //when the user loads the program or has an empty search bar, it will display all the movies
            if (String.IsNullOrEmpty(searchText))
            {
                MoviesList.ItemsSource = allMovies;
                return;
            }
            List<Movie> chosenMovie = new List<Movie>();
            foreach (Movie movie in allMovies)
            {
                bool titleSearch = false;
                bool genreSearch = false;
                //again making the search not case sensitive
                if (!String.IsNullOrEmpty(movie.Title))
                {
                    if (movie.Title.ToLower().Contains(searchText))
                    {
                        titleSearch = true;
                    }
                }
                //filters by search when the user types in a genre or a title(doesn't have to be the full title)
                
                //can't use !String.ISNullOrEmpty for a list as it can only work for strings
                if (movie.Genre != null)
                {
                    foreach (var genre in movie.Genre)
                    {
                        if (genre.ToLower().Contains(searchText))
                        {
                            genreSearch = true;
                        }
                    }
                }
                if (titleSearch || genreSearch)
                {
                    chosenMovie.Add(movie);
                }

            }
            MoviesList.ItemsSource = chosenMovie;
        }
    

    }
}
