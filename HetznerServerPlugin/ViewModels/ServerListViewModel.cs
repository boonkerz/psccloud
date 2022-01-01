using Avalonia.Collections;
using Avalonia.Threading;
using Newtonsoft.Json.Linq;
using PscCloud.Service;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using PscCloud.Plugin.HetznerServerPlugin.Models;
using PscCloud.Plugin.HetznerServerPlugin.Service;
using PscCloud.Plugin.HetznerServerPlugin.Views;
using PscCloud.Shared.Models.ViewModels;
using PscCloud.Shared.Service;
using TabItem = PscCloud.Plugin.HetznerServerPlugin.Models.TabItem;

namespace PscCloud.Plugin.HetznerServerPlugin.ViewModels
{
    class ServerListViewModel : ViewModelBase, IPluginViewModelBase
    {

        public Server selectedServer { get; set; }
        
        public ServerService ServerService { get; set; }

        private TabViewControl TabViewControlModel { get; set; }

        
        public Server SelectedServer
        {
            get
            {
                return this.selectedServer;
            }
            set
            {
                this.selectedServer = value;
            }
        }

        public void updateMetrics()
        {
            var tabItem = new TabItem(this.selectedServer.Name, this.selectedServer);
            var uiDispatcher = Ioc.Default.GetService<IUserInterfaceDispatchService>();
            uiDispatcher.InvokeAsync(() =>
            {
                this.TabViewControlModel.TabItems.Add(tabItem);
                this.TabViewControlModel.SelectedTabServer = tabItem;
            });
        }

        public void OnRefreshStatus()
        {
            ServerService.RefreshStatus();
        }

        public void OnDockerUpdate()
        {
            
        }

        public void OnDockerPrune()
        {
            ServerService.DockerPruneAllServer();
        }

        private void OpenSettings()
        {
            var win = new SettingsView();
            win.DataContext = new SettingsViewModel();
            win.Show();
        }

        public ServerListViewModel()
        {
            TabViewControlModel = new TabViewControl();
            ServerService = new ServerService();
            ServerService.Init();
        }

        public void addTab(Server sender)
        {
            var tabItem = new TabItem(sender.Name, sender);
            var uiDispatcher = Ioc.Default.GetService<IUserInterfaceDispatchService>();
            uiDispatcher.InvokeAsync(() =>
            {
                this.TabViewControlModel.TabItems.Add(tabItem);
                this.TabViewControlModel.SelectedTabServer = tabItem;
            });
        }

        public UserControl GetViewControl()
        {
            return new ServerListView();
        }
    }
}
