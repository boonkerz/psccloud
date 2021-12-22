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
using PscCloud.Service;
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

        public Note _selectedNote;

        public Note SelectedNote
        {
            get => this._selectedNote;
            set => SetProperty(ref this._selectedNote, value);
        }
        
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
        private void DoSync()
        {
            this.loadNotes();
        }
        private void OpenSettings()
        {
            
        }
        async private void loadNotes()
        {
            this.Notes.Clear();
            var appService = Ioc.Default.GetService<AppService>();
            appService.AppIsStartSyncing();
            var api = new Api.Notes();
            var notes = await api.GetNotes(this.settings);
            foreach (var note in notes)
            {
                this.Notes.Add(note);
            }
            appService.AppIsEndSyncing();
            this.SelectedNote = this.Notes.First();
        }

        public UserControl GetViewControl()
        {
            return new NotesListView();
        }
    }
}
