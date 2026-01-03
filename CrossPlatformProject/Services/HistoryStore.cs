using System.Text.Json;

namespace CrossPlatformProject.Services
{
    public static class HistoryStore
    {
        public class HistoryEntry
        {
            public string Action { get; set; } = "";   
            public string Title { get; set; } = "";
            public int Year { get; set; }
            public string Emoji { get; set; } = "";
            public string Genres { get; set; } = "";
            public string TimeStamp { get; set; } = ""; 
        }

        static string GetFile()
        {
            string user = Preferences.Get("LoggedInUser", "guest");
            return Path.Combine(FileSystem.AppDataDirectory, "history_" + user + ".json");
        }

        public static List<HistoryEntry> Load()
        {
            string file = GetFile();

            if (File.Exists(file) == false)
                return new List<HistoryEntry>();

            try
            {
                string json = File.ReadAllText(file);
                List<HistoryEntry> list = JsonSerializer.Deserialize<List<HistoryEntry>>(json);

                if (list == null)
                    return new List<HistoryEntry>();

                return list;
            }
            catch
            {
                return new List<HistoryEntry>();
            }
        }

        public static void Save(List<HistoryEntry> list)
        {
            string file = GetFile();

            string json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(file, json);
        }

        public static void Add(Movie movie, string action)
        {
            if (movie == null)
                return;

            List<HistoryEntry> list = Load();

            string genresText = "";
            if (movie.Genre != null)
            {
                genresText = string.Join(", ", movie.Genre);
            }

            HistoryEntry entry = new HistoryEntry();
            entry.Action = action;
            entry.Title = movie.Title;
            entry.Year = movie.Year;
            entry.Emoji = movie.Emoji;
            entry.Genres = genresText;
            entry.TimeStamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            list.Insert(0, entry);

            Save(list);
        }

        public static void Clear()
        {
            Save(new List<HistoryEntry>());
        }
    }
}
