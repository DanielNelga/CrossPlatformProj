using System.Text.Json;
namespace CrossPlatformProject
{
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
        }

       
    }
}
