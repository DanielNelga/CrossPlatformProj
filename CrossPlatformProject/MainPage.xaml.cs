using System.Text.Json;
using CrossPlatformProject.Services;
namespace CrossPlatformProject
{
    //MainPage
    public partial class MainPage : ContentPage
    {
        //timer
        private IDispatcherTimer _clockTimer;

        //sorting
        private bool _sortAscendingOrder = true;

        //stores list
        private List<Movie> _currentView = new List<Movie>();

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
                    using HttpClient client = new HttpClient();

                    //download if doesn't exist
                    jsonFile = await client.GetStringAsync(jsonFileGithub);
                    File.WriteAllText(localCache, jsonFile);

                }
                else
                {
                    //if exist read form local json
                    jsonFile = File.ReadAllText(localCache);
                }

                //deserialise JSON into a list of Movie objects
                allMovies = JsonSerializer.Deserialize<List<Movie>>(jsonFile) ?? new List<Movie>();

                //initialise current view with all movies
                _currentView = allMovies.ToList();
                MoviesList.ItemsSource = _currentView;


            }

            catch (Exception ex)
            {
                //display in listview
                allMovies = new List<Movie>();
                _currentView = new List<Movie>();
                MoviesList.ItemsSource = _currentView;

                await DisplayAlert("Error", "Could not load movies (internet/cache issue).", "OK");


            }

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

            //store movies that match the search
            List<Movie> chosenMovie = new List<Movie>();

            //loop through movies to chekc for matches
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


                //filters movies, checks to see if genre or search match
                if (titleSearch || genreSearch)
                {
                    chosenMovie.Add(movie);
                }

            }

            //update with new movies after search
            _currentView = chosenMovie;
            MoviesList.ItemsSource = _currentView;

        }


        //When user clicks on movie in list, this event gets called
        private async void MoviesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //ensures the item is a movie object
            if (e.Item is Movie selectedMovie)
            {
                //add entry for viewing movie into history
                HistoryStore.Add(selectedMovie, "Viewed");

                //pass the selected movie to the detail page using navigation parameters
                var parameters = new Dictionary<string, object>
                {
                    //key and value
                    {"Movie", selectedMovie }
                };

                //nav to moviedetailspage with the selected movie
                await Shell.Current.GoToAsync(nameof(MovieDetailPage), parameters);

                //clear selection highlight in the list view
                ((ListView)sender).SelectedItem = null;
            }
        }

        
        private async void Settings_Clicked(object sender, EventArgs e)
        {
            //goes straight to settings as the 

            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        private async void Favourites_Clicked(object sender, EventArgs e)
        {
            //goes straight to favourites as the // skips all the previous pages

             await Shell.Current.GoToAsync(nameof(FavouritesPage));
        }
        protected override async void OnAppearing()
        {

            base.OnAppearing();

            //fade in animation
            MoviesList.Opacity = 0;
            await Task.Delay(120);
            await MoviesList.FadeTo(1,1150, Easing.CubicInOut);

            //load settings
            var settings = ManageSettings.Load() ?? new SettingsList();

            //hide clock
            clockLabel.IsVisible = settings.ShowClock;

            AppClock();

            //cretae and start timer to update clock
            _clockTimer = Dispatcher.CreateTimer();
            _clockTimer.Interval = TimeSpan.FromSeconds(1);
            _clockTimer.Tick += (s, e) => AppClock();
            _clockTimer.Start();


        }

        //called when page not visible
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //stop timer/clock
            _clockTimer?.Stop();

            //reset opacity for the fade animation again
            MoviesList.Opacity = 0;
        }

        //Displaying the live clock
        private void AppClock()
        {
            if(clockLabel != null) 
            clockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        //sorts by alphabetical order
        private void SortButton_Clicked(object sender, EventArgs e)
        {
            //if no movies in view, return
            if (_currentView == null || _currentView.Count == 0)
                return;

            //sort A-Z
            if (_sortAscendingOrder)
            {
                //sorts in order by title, use empty string if title is null
                _currentView = _currentView
                    .OrderBy(m => m.Title ?? string.Empty)
                    .ToList();

                //update button
                SortButton.Text = "Z–A";
            }
            else
            {
                //sorts in order by title, use empty string if title is null
                _currentView = _currentView
                    .OrderByDescending(m => m.Title ?? string.Empty)
                    .ToList();

                SortButton.Text = "A–Z";
            }

            //flip the sort direction for next time
            _sortAscendingOrder = !_sortAscendingOrder;

            //refreshes the page
            MoviesList.ItemsSource = _currentView;
        }

       
    }
}
