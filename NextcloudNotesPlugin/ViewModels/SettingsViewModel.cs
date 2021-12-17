using System.Reactive;
using System.Reactive.Disposables;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PscCloud.Plugin.Nextcloud.Notes.Api;
using PscCloud.Plugin.Nextcloud.Notes.Api.Model.Login;
using PscCloud.Shared.Models.ViewModels;
using PscCloud.Shared.Settings;

namespace PscCloud.Plugin.Nextcloud.Notes.ViewModels
{
    public class SettingsViewModel: ViewModelBase
    {
        public string Server {get; set; }
        public string LoginName {get; set; }
        public string AppPassword {get; set; }

        private Login loginApi = new Login();

        public SettingsViewModel()
        {
            loginApi.LoginFinished += LoginApiOnLoginFinished;
        }

        private void LoginApiOnLoginFinished(object? sender, EventPollResponse e)
        {
            PollResponse poll = e.PollResponse;
            this.LoginName = poll.loginName;
            this.AppPassword = poll.appPassword;
            this.Server = poll.server;

            var settings = new Model.Settings();
            settings.Server = this.Server;
            settings.LoginName = this.LoginName;
            settings.AppPassword = this.AppPassword;
            var settingsManager = Ioc.Default.GetService<SettingsManager>();
            settingsManager.SavePluginSettings("NextcloudNotesPlugin", settings);
        }

        public void LoginAction()
        {
            loginApi.LoginFlow(this.Server);
        }
    }
}