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
        private bool isLoading = false;

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
                Task.Run(async () => await this.loadNotes());
            }
            
            autoSaveTimer.Tick += new EventHandler(reloadNodes);
            autoSaveTimer.Interval = new TimeSpan(0, this.settingsManager.CoreSettings.ApplicationUpdateFrequencyMinutes, 0);
            autoSaveTimer.Start();
        }
        
        private void reloadNodes(object sender, EventArgs e)
        {
            Task.Run(async () => await this.loadNotes());
        }
        
        async private void DoSync()
        {
            await this.loadNotes();
        } async private void DoSave()
        {
            await this.saveNote();
        }
        private void OpenSettings()
        {
            var win = new SettingsView();
            win.DataContext = new SettingsViewModel();
            win.Show();
        }
        async private Task saveNote()
        {
            var appService = Ioc.Default.GetService<AppService>();
            appService.AppIsStartSyncing();
            var api = new Api.Notes();
            var saved = await api.SaveNote(this.settings, this.SelectedNote);
            appService.AppIsEndSyncing();
        }
        async private Task loadNotes()
        {
            if (isLoading) return;
            isLoading = true;
            string selectedNodeId = null;
            if (SelectedNote != null)
            {
                selectedNodeId = SelectedNote.Id;
            }

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
            if (selectedNodeId != null)
            {
                foreach (var node in this.Notes)
                {
                    if (selectedNodeId == node.Id)
                    {
                        this.SelectedNote = node;
                    } 
                }
            }
            else
            {
                this.SelectedNote = this.Notes.First();    
            }
            
            isLoading = false;
        }

        public UserControl GetViewControl()
        {
            return new NotesListView();
        }
    }
}
