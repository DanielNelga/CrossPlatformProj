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

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("director")]
        public string Director { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("emoji")]
        public string Emoji { get; set; }

        public string GenreDisplay
        { 
            get
            {
                if (Genre == null)
                    return "";

                return string.Join(", ", Genre);
            }
        }   
    }
}
