using System.Text.Json;

namespace CrossPlatformProject.Services
{
    //history stored locally in Json per user
    public static class HistoryStore
    {

        //All things that are visible when viewing history
        public class HistoryEntry
        {
            public string Action { get; set; } = "";   
            public string Title { get; set; } = "";
            public int Year { get; set; }
            public string Emoji { get; set; } = "";
            public string Genres { get; set; } = "";
            public string TimeStamp { get; set; } = ""; 
        }

        //Each user has their own history
        static string GetFile()
        {
            //get the currently logged in user(defaults to guest)
            string user = Preferences.Get("LoggedInUser", "guest");

            //return path to the users Json file
            return Path.Combine(FileSystem.AppDataDirectory, "history_" + user + ".json");
        }

        //loads user's history, if null, returns empty histry(not to crash)
        public static List<HistoryEntry> Load()
        {
            string file = GetFile();

            //returns empty list if null
            if (File.Exists(file) == false)
                return new List<HistoryEntry>();


            try
            {
                string json = File.ReadAllText(file);

                //deserialise Json into a list of historyentry objects
                List<HistoryEntry> list = JsonSerializer.Deserialize<List<HistoryEntry>>(json);

                //handles null 
                if (list == null)
                    return new List<HistoryEntry>();

                //loaded history
                return list;
            }
            catch
            {
                //if errors or null, reutnrs empty history
                return new List<HistoryEntry>();
            }
        }

        //saves history to local storage
        public static void Save(List<HistoryEntry> list)
        {
            string file = GetFile();

            //serialise history into json
            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                //makes readable
                WriteIndented = true
            });

            //writes data to json file
            File.WriteAllText(file, json);
        }

        //adds a new entry for the action that was done(Viewed or Favourited)
        public static void Add(Movie movie, string action)
        {
            //ensures object is valid
            if (movie == null)
                return;

            //loads history
            List<HistoryEntry> list = Load();

            //converts genre array into strings
            string genresText = "";
            if (movie.Genre != null)
            {
                genresText = string.Join(", ", movie.Genre);
            }

            //new entries
            HistoryEntry entry = new HistoryEntry();
            entry.Action = action;
            entry.Title = movie.Title;
            entry.Year = movie.Year;
            entry.Emoji = movie.Emoji;
            entry.Genres = genresText;
            entry.TimeStamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            //latest entry at the top
            list.Insert(0, entry);

            //save the list
            Save(list);
        }

        //clears all history
        public static void Clear()
        {
            Save(new List<HistoryEntry>());
        }
    }
}
