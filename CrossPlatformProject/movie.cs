using System.Text.Json.Serialization;

namespace CrossPlatformProject
{
    public partial class Movie
    {
        //Means to search and capture data from the json file
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("genre")]
        public List<string> Genre { get; set; }
        [JsonPropertyName("emoji")]
        public string Emoji { get; set; }
    }
}
