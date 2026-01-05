using System.Text.Json.Serialization;

namespace CrossPlatformProject
{
    public partial class Movie
    {
        //Means to search and capture data from the json file
        [JsonPropertyName("title")]
        //movie title(got from title in JSON)
        public string Title { get; set; }

        //genre (got from title in JSON)
        [JsonPropertyName("genre")]
        public List<string> Genre { get; set; }

        //year (got from title in JSON)
        [JsonPropertyName("year")]
        public int Year { get; set; }

        //director (got from title in JSON)
        [JsonPropertyName("director")]
        public string Director { get; set; }

        // rating(got from title in JSON)
        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        //emoji (got from title in JSON)
        [JsonPropertyName("emoji")]
        public string Emoji { get; set; }

        //display genre (got from title in JSON)
        public string GenreDisplay
        { 
            get
            {
                if (Genre == null)
                    return "";

                //combine genre list into a single display friendly string
                return string.Join(", ", Genre);
            }
        }   
    }
}
