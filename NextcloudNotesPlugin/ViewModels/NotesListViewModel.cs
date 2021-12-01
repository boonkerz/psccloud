using Avalonia.Collections;
using Avalonia.Threading;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PscCloud.Plugin.Nextcloud.Notes.Views;
using PscCloud.Shared.Models.ViewModels;
using PscCloud.Shared.Service;
using PscCloud.Shared.Settings;

namespace PscCloud.Plugin.Nextcloud.Notes.ViewModels
{
    class NotesListViewModel : ViewModelBase, IPluginViewModelBase
    {
        public SettingsManager settings;

        private readonly DispatcherTimer autoSaveTimer = new DispatcherTimer();
        public NotesListViewModel(SettingsManager settingsManager)
        {
            settings = settingsManager;

            var settingsNext = new NextcloudApi.Settings();
            settingsNext.Username = "boonkerz";
            settingsNext.ApplicationName = "PscCloud";
            settingsNext.ServerUri = new Uri("https://cloud.thomas-peterson.de");
            var api = new NextcloudApi.Api(settingsNext);

            api.LoginOrRefreshIfRequiredAsync();
        }
        public UserControl GetViewControl()
        {
            return new NotesListView();
        }
    }
}
