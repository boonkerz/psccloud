using System.Reactive;
using System.Reactive.Disposables;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PscCloud.Plugin.HetznerServerPlugin.Models;
using PscCloud.Shared.Models.ViewModels;
using PscCloud.Shared.Settings;

namespace PscCloud.Plugin.HetznerServerPlugin.ViewModels
{
    public class SettingsViewModel: ViewModelBase
    {
        public string ApiToken { get; set; }
        public string DNSApiToken { get; set; }
        public string SSHFile { get; set; }

        public SettingsViewModel()
        {
            this.loadData();
        }
        private void loadData()
        {
            var settings = new Settings();
            var settingsManager = Ioc.Default.GetService<SettingsManager>();
            settingsManager.LoadPluginSettings("HetznerServerPlugin", settings);

            if (!string.IsNullOrEmpty(settings.ApiToken))
            {
                this.ApiToken = settings.ApiToken;
                this.DNSApiToken = settings.DNSApiToken;
                this.SSHFile = settings.SSHFile;
            }
        }
        public void SaveAction()
        {
            var settings = new Settings();
            settings.ApiToken = this.ApiToken;
            settings.DNSApiToken = this.DNSApiToken;
            settings.SSHFile = this.SSHFile;
            var settingsManager = Ioc.Default.GetService<SettingsManager>();
            settingsManager.SavePluginSettings("HetznerServerPlugin", settings);
        }
    }
}