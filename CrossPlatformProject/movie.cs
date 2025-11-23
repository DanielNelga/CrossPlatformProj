using System.Text.Json.Serialization;

namespace CrossPlatformProject
{
    public partial class Movie
    {
        //Means to search and capture data from the json file
        public string Title { get; set; }
        public List<string> Genre { get; set; }
        public string Emoji { get; set; }
    }
}
