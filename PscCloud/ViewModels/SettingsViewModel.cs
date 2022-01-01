using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PscCloud.Shared.Models.ViewModels;
using PscCloud.Shared.Settings;

namespace PscCloud.ViewModels
{
    public partial class SettingsViewModel : ViewModelBase
    {

        private SettingsManager SettingsManager { get; }

        public SettingsViewModel(SettingsManager settingsManager)
        {
            this.SettingsManager = settingsManager;

        }

        public int ApplicationUpdateFrequencyMinutes
        {
            get
            {
                return this.SettingsManager.CoreSettings.ApplicationUpdateFrequencyMinutes;
            }

            set
            {
                this.SettingsManager.CoreSettings.ApplicationUpdateFrequencyMinutes = value;
                this.SettingsManager.SaveSettings();
            }
        }
    }
}
