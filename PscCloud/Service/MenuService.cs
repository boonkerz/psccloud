using System;
using Avalonia.Collections;
using MicroCubeAvalonia.Controls;
using MicroCubeAvalonia.IconPack;
using MicroCubeAvalonia.IconPack.Icons;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PscCloud.Shared.Service;
using PscCloud.ViewModels;
using PscCloud.Views;
using ReactiveUI;

namespace PscCloud.Service
{
    public class MenuService: ReactiveObject, IMenuService
    {
        private AvaloniaList<HamburgerMenuItem> menuItems = new AvaloniaList<HamburgerMenuItem>();
        private AvaloniaList<HamburgerMenuItem> menuOptionItems = new AvaloniaList<HamburgerMenuItem>();
        
        public event EventHandler MenuChanged;
        
        public AvaloniaList<HamburgerMenuItem> MenuItems
        {
            get => this.menuItems;
            private set => this.RaiseAndSetIfChanged(ref this.menuItems, value);
        }

        /// <summary>
        /// Gets the list of options items shown in the hamburger menu (at the bottom).
        /// </summary>
        public AvaloniaList<HamburgerMenuItem> MenuOptionItems
        {
            get => this.menuOptionItems;
            private set => this.RaiseAndSetIfChanged(ref this.menuOptionItems, value);
        }

        /// <summary>
        /// Gets or sets the currently selected Hamburger Menu Tab.
        /// </summary>
        
        public void CreateMenuItemsRegular()
        {
            this.MenuItems.Clear();
            this.MenuOptionItems.Clear();
            
            var pluginTab = new HamburgerMenuItem()
            {
                Icon = new IconControl() { BindableKind = PackIconMaterialKind.PowerPlug },
                Label = "Plugins",
                ToolTip = "View Plugin Records",
                Tag = Ioc.Default.GetService<PluginListViewModel>(),
            };
            var settingsTab = new HamburgerMenuItem()
            {
                Icon = new IconControl() { BindableKind = PackIconMaterialKind.SettingsOutline },
                Label = "Settings",
                ToolTip = "Settings",
                Tag = Ioc.Default.GetService<SettingsViewModel>(),
            };

            this.MenuOptionItems.Add(pluginTab);
            this.MenuOptionItems.Add(settingsTab);
            OnMenuChanged(EventArgs.Empty);
        }

        public void AddMenuItem(HamburgerMenuItem item)
        {
            this.MenuItems.Add(item);
            OnMenuChanged(EventArgs.Empty);
        }
        
        public void CreateSettingsItemsRegular()
        {
            this.MenuItems.Clear();
            this.MenuOptionItems.Clear();

            var settingsTab = new HamburgerMenuItem()
            {
                Icon = new IconControl() { BindableKind = PackIconMaterialKind.SettingsOutline },
                Label = "Settings",
                ToolTip = "Settings",
                //Tag = this.SettingsViewModel,
            };
           
            this.MenuOptionItems.Add(settingsTab);
            
            OnMenuChanged(EventArgs.Empty);
        }

        protected virtual void OnMenuChanged(EventArgs e)
        {
            MenuChanged?.Invoke(this, e);
        }
    }
}