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
using PscCloud.Plugin.Nextcloud.Notes.Api.Model.Notes;
using PscCloud.Plugin.Nextcloud.Notes.Views;
using PscCloud.Shared.Models.ViewModels;
using PscCloud.Shared.Service;
using PscCloud.Shared.Settings;
using ScottPlot;
using Settings = PscCloud.Plugin.Nextcloud.Notes.Model.Settings;

namespace PscCloud.Plugin.Nextcloud.Notes.ViewModels
{
    public class NotesListViewModel : ViewModelBase, IPluginViewModelBase
    {
        public SettingsManager settingsManager;
        
        public AvaloniaList<Note> Notes { get; set; }
        
        private Settings settings { get; set; }

        private readonly DispatcherTimer autoSaveTimer = new DispatcherTimer();
        public NotesListViewModel(SettingsManager settingsManager)
        {
            this.Notes = new AvaloniaList<Note>();
            this.settings = new Model.Settings();
            this.settingsManager = settingsManager;

            this.settingsManager.LoadPluginSettings("NextcloudNotesPlugin", this.settings);

            if (this.settings.AppPassword == null || this.settings.AppPassword.Length == 0)
            {
                var win = new SettingsView();
                win.DataContext = new SettingsViewModel();
                win.Show();
            }
            else
            {
                this.loadNotes();
            }
        }

        private void loadNotes()
        {
            var api = new Api.Notes();
            var notes = api.GetNotes(this.settings);
            foreach (var note in notes)
            {
                this.Notes.Add(note);
            }
        }

        public UserControl GetViewControl()
        {
            return new NotesListView();
        }
    }
}
