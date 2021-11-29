
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using MicroCubeAvalonia.Controls;
using MicroCubeAvalonia.IconPack;
using MicroCubeAvalonia.IconPack.Icons;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Prise.Plugin;
using PscCloud.Plugin.Contract;
using PscCloud.Plugin.HetznerVolumePlugin.ViewModels;
using PscCloud.Plugin.HetznerVolumePlugin.Views;
using PscCloud.Service;
using PscCloud.Shared.Service;
using PscCloud.Shared.Settings;

namespace PscCloud.Plugin.HetznerVolumePlugin
{

    [Plugin(PluginType = typeof(IAppPlugin))]
    public class HetznerVolumePlugin : IAppPlugin
    {
        public string GetName()
        {
            return "HetznerVolumePlugin";
        }

        public void addMenu()
        {
            MenuService service = Ioc.Default.GetService<MenuService>();
            
            var volumeTab = new HamburgerMenuItem()
            {
                Icon = new IconControl() { BindableKind = PackIconMaterialKind.Harddisk },
                Label = "Volume",
                ToolTip = "View Volumes",
                Tag = new VolumeListViewModel(Ioc.Default.GetService<SettingsManager>()),
            };

            service.AddMenuItem(volumeTab);
        }
    }

}