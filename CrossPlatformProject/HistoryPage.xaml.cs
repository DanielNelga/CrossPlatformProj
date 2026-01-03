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

            HistoryList.ItemsSource = HistoryStore.Load();
        }

        private async void Clear_Clicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert(
                "Clear History",
                "Do you really want to clear your history?",
                "Yes",
                "No"
            );

            if (confirm == false)
                return;

            HistoryStore.Clear();
            HistoryList.ItemsSource = HistoryStore.Load();
        }

        private async void BackToMainPage_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void Favourites_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("FavouritesPage");
        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("Setting");
        }

    }
}
