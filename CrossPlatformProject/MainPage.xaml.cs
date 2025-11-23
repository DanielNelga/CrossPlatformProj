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

        //creating the list and then w
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


       
    }
}
