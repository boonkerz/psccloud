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
using PscCloud.Plugin.Nextcloud.Notes.ViewModels;
using PscCloud.Service;
using PscCloud.Shared.Service;
using PscCloud.Shared.Settings;

namespace PscCloud.Plugin.Nextcloud.Notes
{

    [Plugin(PluginType = typeof(IAppPlugin))]
    public class NextcloudNotesPlugin : IAppPlugin
    {
        public string GetName()
        {
            return "NextcloudNotesPlugin";
        }

        public void addMenu()
        {
            MenuService service = Ioc.Default.GetService<MenuService>();
            
            var volumeTab = new HamburgerMenuItem()
            {
                Icon = new IconControl() { BindableKind = PackIconMaterialKind.Notebook },
                Label = "Notes",
                ToolTip = "View Notes",
                Tag = new NotesListViewModel(Ioc.Default.GetService<SettingsManager>()),
            };

            service.AddMenuItem(volumeTab);
        }
    }

}