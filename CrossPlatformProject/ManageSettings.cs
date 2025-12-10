

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
            //sets default settings at the beginning and then should save settings into the MovieSettings file and then takes settings from there
            if(!File.Exists(settingsSaved))
                return new SettingsList();

            string settingsJson = File.ReadAllText(settingsSaved);

            //incase something happened to the file, this will prevent it from crashing
            return JsonSerializer.Deserialize<SettingsList>(settingsJson) ?? new SettingsList();
        }
        //creating the file of settings
        public static void Save(SettingsList settings) 
        {
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
            File.WriteAllText(settingsSaved, json);
        }
    }
}
