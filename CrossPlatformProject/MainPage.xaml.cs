using System.Text.Json;
namespace CrossPlatformProject
{
    //MainPage
    public partial class MainPage : ContentPage
    {
        private IDispatcherTimer _clockTimer;
        private bool _sortAscendingOrder = true;
        private List<Movie> _currentView = new List<Movie>();

        //using the json API can getting movies+movie objects from here
        string jsonFileGithub = "https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json";

        //storing in local cache here
        string localCache = Path.Combine(FileSystem.AppDataDirectory, "movies.json");

        //creating the list and using the external Movie class
        List<Movie> allMovies = new List<Movie>();

        public MainPage()
        {
            System.Diagnostics.Debug.WriteLine("MainPage constructor started");
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("MainPage InitializeComponent finished");
            LoadMovies();
        }

        private async void LoadMovies()
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

                 allMovies = JsonSerializer.Deserialize<List<Movie>>(jsonFile) ?? new List<Movie>();
                 _currentView = allMovies.ToList();
                 MoviesList.ItemsSource = _currentView;




        }
        private void Search(object sender, TextChangedEventArgs e)
        {
            //making search not case sensitive by using .ToLower();
            string searchText = e.NewTextValue.ToLower();

            //when the user loads the program or has an empty search bar, it will display all the movies
            if (String.IsNullOrEmpty(searchText))
            {
                _currentView = allMovies.ToList();
                MoviesList.ItemsSource = _currentView;
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


                //filters movies
                if (titleSearch || genreSearch)
                {
                    chosenMovie.Add(movie);
                }

            }


            _currentView = chosenMovie;
            MoviesList.ItemsSource = _currentView;

        }


        //When user clicks on movie in list, this event gets called
        private async void MoviesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Movie selectedMovie)
            {
                var parameters = new Dictionary<string, object>
                {
                    //key and value
                    {"Movie", selectedMovie }
                };

                await Shell.Current.GoToAsync(nameof(MovieDetailPage), parameters);

                ((ListView)sender).SelectedItem = null;
            }
        }

        
        private async void Settings_Clicked(object sender, EventArgs e)
        {
            //goes straight to settings as the 

            await Shell.Current.GoToAsync("Setting");
        }

        private async void BackToMainPage_Clicked(object sender, EventArgs e)
        {
            //goes straight to mainpage as the // skips all the previous pages

            await Shell.Current.GoToAsync("MainPage");

        }

        private async void Favourites_Clicked(object sender, EventArgs e)
        {
            //goes straight to favourites as the // skips all the previous pages

            await Shell.Current.GoToAsync("FavouritesPage");
        }
        protected override void OnAppearing()
        {

            base.OnAppearing();
           

            AppClock();

            _clockTimer = Dispatcher.CreateTimer();
            _clockTimer.Interval = TimeSpan.FromSeconds(1);
            _clockTimer.Tick += (s, e) => AppClock();
            _clockTimer.Start();


        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _clockTimer?.Stop();
        }

        //Displaying the live clock
        private void AppClock()
        {
            clockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void SortButton_Clicked(object sender, EventArgs e)
        {
            if (_currentView == null || _currentView.Count == 0)
                return;

            if (_sortAscendingOrder)
            {
                _currentView = _currentView
                    .OrderBy(m => m.Title ?? string.Empty)
                    .ToList();

                SortButton.Text = "Z–A";
            }
            else
            {
                _currentView = _currentView
                    .OrderByDescending(m => m.Title ?? string.Empty)
                    .ToList();

                SortButton.Text = "A–Z";
            }

            _sortAscendingOrder = !_sortAscendingOrder;

            MoviesList.ItemsSource = _currentView;
        }

       
    }
}
