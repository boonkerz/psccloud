using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Collections;
using lkcode.hetznercloudapi.Api;
using MicroCubeAvalonia.Controls;
using MicroCubeAvalonia.IconPack;
using MicroCubeAvalonia.IconPack.Icons;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PscCloud.Service;
using PscCloud.Shared.Models.ViewModels;
using PscCloud.Shared.Service;
using PscCloud.Shared.Settings;

namespace PscCloud.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private SettingsManager SettingsManager { get; set; }
        private AppService AppService { get; set; }

        
        private AvaloniaList<HamburgerMenuItem> menuItems = new AvaloniaList<HamburgerMenuItem>();
        private AvaloniaList<HamburgerMenuItem> menuOptionItems = new AvaloniaList<HamburgerMenuItem>();
        private HamburgerMenuItem selectedItem;

        private SettingsViewModel SettingsViewModel { get; set; }
        
        private PluginListViewModel PluginListViewModel { get; set; }
        private bool isRefreshing = false;
        private bool isReconnecting = false;
        public MenuService menuService;

        public AvaloniaList<HamburgerMenuItem> MenuItems
        {
            get => this.menuItems;
            private set => SetProperty(ref this.menuItems, value);
        }

        public AvaloniaList<HamburgerMenuItem> MenuOptionItems
        {
            get => this.menuOptionItems;
            private set => SetProperty(ref this.menuOptionItems, value);
        }

        public HamburgerMenuItem SelectedItem
        {
            get => this.selectedItem;
            set
            {
                if (value.Label is not null)
                {
                    this.SettingsManager.CoreSettings.ActiveMenuItem = value.Label;
                }
                SetProperty(ref this.selectedItem, value);
            }
        }

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            private set => SetProperty(ref this.isReconnecting, value);
        }

        public bool IsReconnecting
        {
            get => this.isReconnecting;
            private set => SetProperty(ref this.isReconnecting, value);
        }
        
        public MenuService MenuService
        {
            get => this.menuService;
            private set => SetProperty(ref this.menuService, value);
        }

        public MainWindowViewModel()
        {
            Startup.RegisterServices();

            this.menuService = Ioc.Default.GetService<MenuService>();
            this.SettingsManager = Ioc.Default.GetService<SettingsManager>();
            this.AppService = Ioc.Default.GetService<AppService>();
            this.AppService.AppStartSyncing += ServerService_SyncStarted;
            this.AppService.AppEndSyncing += ServerService_SyncCompleted;
            this.menuService.MenuChanged += delegate(object? sender, EventArgs args)
            {
                this.MenuItems = menuService.MenuItems;
                this.MenuOptionItems = menuService.MenuOptionItems;
            };
            
            this.AppService.AppStarted += delegate(object? sender, EventArgs args)
            {
                if (!string.IsNullOrEmpty(this.SettingsManager.CoreSettings.ActiveMenuItem))
                {
                    var item = menuService.GetMenuOptionByLabel(this.SettingsManager.CoreSettings.ActiveMenuItem);
                    if (item != null)
                    {
                        this.SelectedItem = item;
                    }
                }
                else
                {
                    this.SelectedItem = menuService.MenuOptionItems.Last();
                }
            };
            
            this.InitializeClient();
        }
        
        

        private void ServerService_SyncStarted(object? sender, EventArgs e)
        {
            this.IsReconnecting = true;
        }

        private void ServerService_SyncCompleted(object? sender, EventArgs e)
        {
            this.IsReconnecting = false;
        }

        async private void InitializeClient()
        {
            this.SettingsViewModel = Ioc.Default.GetService<SettingsViewModel>();

            this.menuService.CreateMenu();
            
        }
        
    }
}
