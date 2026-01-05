using System.Text.Json;

namespace CrossPlatformProject
{
    public static class ManageSettings
    {
        //psth way to file on the device
        static string settingsSaved = Path.Combine(FileSystem.AppDataDirectory, "MovieSettings.json");

        //loading preferences by reading from the file
        public static SettingsList Load()
        {
            try 
            {
                //sets default settings at the beginning and then should save settings into the MovieSettings file and then takes settings from there
                if (!File.Exists(settingsSaved))
                    return new SettingsList();

                var settingsJson = File.ReadAllText(settingsSaved);

                //incase something happened to the file, this will prevent it from crashing
                return JsonSerializer.Deserialize<SettingsList>(settingsJson) ?? new SettingsList();
            }

            catch 
            { 
                return new SettingsList();
            }
        }


        //creating the file of settings
        public static void Save(SettingsList settings) 
        {
            //convert settings object into json format and make it readable
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
            {
                WriteIndented = true,
            });

            //write json string to settings file
            File.WriteAllText(settingsSaved, json);
        }
    }
}
