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

        public String ApiToken
        {
            get
            {
                return this.SettingsManager.CoreSettings.ApiToken;
            }

            set
            {
                this.SettingsManager.CoreSettings.ApiToken = value;
                this.SettingsManager.SaveSettings();
            }
        }

        public String DNSApiToken
        {
            get
            {
                return this.SettingsManager.CoreSettings.DNSApiToken;
            }

            set
            {
                this.SettingsManager.CoreSettings.DNSApiToken = value;
                this.SettingsManager.SaveSettings();
            }
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

        public String SSHFile
        {
            get
            {
                return this.SettingsManager.CoreSettings.SSHFile;
            }

            set
            {
                this.SettingsManager.CoreSettings.SSHFile = value;
                this.SettingsManager.SaveSettings();
            }
        }
    }
}
