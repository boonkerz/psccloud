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
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PscCloud.Plugin.Nextcloud.Notes.Api;
using PscCloud.Plugin.Nextcloud.Notes.Views;
using PscCloud.Shared.Models.ViewModels;
using PscCloud.Shared.Service;
using PscCloud.Shared.Settings;
using ScottPlot;

namespace PscCloud.Plugin.Nextcloud.Notes.ViewModels
{
    public class NotesListViewModel : ViewModelBase, IPluginViewModelBase
    {
        public SettingsManager settingsManager;
        
        private readonly DispatcherTimer autoSaveTimer = new DispatcherTimer();
        public NotesListViewModel(SettingsManager settingsManager)
        {
            this.settingsManager = settingsManager;

            var settings = new Model.Settings();
            this.settingsManager.LoadPluginSettings("NextcloudNotesPlugin", settings);

            if (settings.AppPassword == null || settings.AppPassword.Length == 0)
            {
                var win = new SettingsView();
                win.Show();
            }
        }

        public UserControl GetViewControl()
        {
            return new NotesListView();
        }
    }
}
