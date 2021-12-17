using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PscCloud.Shared.Settings
{
    public class SettingsManager
    {
        private string DataRoot => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PSC", "PscCloud");

        private string SettingsPath => Path.Combine(this.DataRoot, "settings.json");
        
        public SettingsManager()
        {
            Directory.CreateDirectory(this.DataRoot);
            this.LoadSettings();
        }

        /// <summary>
        /// Gets or sets the core settings instance.
        /// </summary>
        public CoreSettings CoreSettings { get; set; } = new CoreSettings();

        /// <summary>
        /// Gets or sets the UI Settings instance.
        /// </summary>
       // public UISettings UISettings { get; set; } = new UISettings();

        private string SettingsFile { get; set; }

        /// <summary>
        /// Reads the configuration file.
        /// </summary>
        public void LoadSettings()
        {
            if (File.Exists(this.SettingsPath))
            {
                string json = File.ReadAllText(this.SettingsPath);
                JsonConvert.PopulateObject(json, this);
            }
        }

        /// <summary>
        /// Saves the configuration file.
        /// </summary>
        public void SaveSettings()
        {
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(this.SettingsPath))
            {
                JsonSerializer serializer = new JsonSerializer()
                {
                    Formatting = Formatting.Indented,
                };

                serializer.Serialize(file, this);
            }
        }
        
        public void LoadPluginSettings(string pluginName, object? settingsObject)
        {

            var pluginSettingsPath = Path.Combine(this.DataRoot, pluginName + "_settings.json");
            
            if (File.Exists(pluginSettingsPath))
            {
                string json = File.ReadAllText(pluginSettingsPath);
                JsonConvert.PopulateObject(json, settingsObject);
            }
        }
        
        public void SavePluginSettings(string pluginName, object? settingsObject)
        {
            var pluginSettingsPath = Path.Combine(this.DataRoot, pluginName + "_settings.json");
            
            using (StreamWriter file = File.CreateText(pluginSettingsPath))
            {
                JsonSerializer serializer = new JsonSerializer()
                {
                    Formatting = Formatting.Indented,
                };

                serializer.Serialize(file, settingsObject);
            }
        }
    }
}
