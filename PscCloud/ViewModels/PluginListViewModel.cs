using Avalonia.Collections;
using Avalonia.Threading;
using Newtonsoft.Json.Linq;
using PscCloud.Service;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Prise;
using PscCloud.Plugin.Contract;
using PscCloud.Shared.Models.ViewModels;
using PscCloud.Shared.Service;

namespace PscCloud.ViewModels
{
    class PluginListViewModel : ViewModelBase
    {
        public RelayCommand<object> LoadAllPluginsCommand { get; set; }
        public RelayCommand<object> UnLoadAllPluginsCommand { get; set; }
        public RelayCommand<object> LoadPluginCommand { get; set; }
        public AvaloniaList<String> Plugins { get; set; }
        public UserControl CurrentControl { get; set; }
        
        private PluginService _pluginService { get; }
        
        public PluginListViewModel(PluginService pluginService)
        {
            this.Plugins = new AvaloniaList<string>();
            
            LoadPluginCommand = new RelayCommand<object>(LoadPlugin);
            LoadAllPluginsCommand = new RelayCommand<object>(LoadAllPlugins);
            UnLoadAllPluginsCommand = new RelayCommand<object>(UnLoadAllPlugins);
            
            foreach (var plugin in pluginService.GetPlugins())
            {
                this.Plugins.Add(plugin.Key);
            }

            this._pluginService = pluginService;
        }
        
        async void LoadPlugin(object parameter)
        {
            this._pluginService.LoadPlugin(parameter.ToString());
        }

        async void LoadAllPlugins(object parameter)
        {
            foreach (var plugin in Plugins)
            {
                this._pluginService.LoadPlugin(plugin);
            }
        }

        async void UnLoadAllPlugins(object parameter)
        {
            
        }
        
    }
}
