using CrossPlatformProject.Services;

namespace CrossPlatformProject
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //load history entries and bind them to the listview
            HistoryList.ItemsSource = HistoryStore.Load();
        }

        //clears history after clicked
        private async void Clear_Clicked(object sender, EventArgs e)
        {

            //confirmation of clear
            bool confirm = await DisplayAlert(
                "Clear History",
                "Do you really want to clear your history?",
                "Yes",
                "No"
            );

            if (confirm == false)
                return;

            //clear the history
            HistoryStore.Clear();

            //refreshes the history
            HistoryList.ItemsSource = HistoryStore.Load();
        }

        //navigate back to the mainpage
        private async void BackToMainPage_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        //navigate back to the favourites page
        private async void Favourites_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("FavouritesPage");
        }

        //navigate back to the settingsPage
        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("Setting");
        }

    }
}
