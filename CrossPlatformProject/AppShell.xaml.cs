namespace CrossPlatformProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //navigation betwen pages
            Routing.RegisterRoute(nameof(MovieDetailPage), typeof(MovieDetailPage));
        }

        
    }
}
