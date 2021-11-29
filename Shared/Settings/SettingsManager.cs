﻿using Newtonsoft.Json;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsManager"/> class.
        /// </summary>
        /// <param name="databaseName">The name of the database file to open.</param>
        public SettingsManager(string databaseName)
        {
            this.SettingsFile = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
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
            if (File.Exists(this.SettingsFile))
            {
                string json = File.ReadAllText(this.SettingsFile);
                JsonConvert.PopulateObject(json, this);
            }
        }

        /// <summary>
        /// Saves the configuration file.
        /// </summary>
        public void SaveSettings()
        {
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(this.SettingsFile))
            {
                JsonSerializer serializer = new JsonSerializer()
                {
                    Formatting = Formatting.Indented,
                };

                serializer.Serialize(file, this);
            }
        }
    }
}
