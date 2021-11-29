using Avalonia.Controls;
using MicroCubeAvalonia.Controls;
using MicroCubeAvalonia.IconPack;
using MicroCubeAvalonia.IconPack.Icons;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Prise.Plugin;
using PscCloud.Plugin.Contract;
using PscCloud.Plugin.HetznerDNSPlugin.ViewModels;
using PscCloud.Service;
using PscCloud.Shared.Settings;

namespace PscCloud.Plugin.HetznerDNSPlugin
{

    [Plugin(PluginType = typeof(IAppPlugin))]
    public class HetznerDNSPlugin : IAppPlugin
    {
        public string GetName()
        {
            return "HetznerDNSPlugin";
        }

        public void addMenu()
        {
            MenuService service = Ioc.Default.GetService<MenuService>();
            
            var volumeTab = new HamburgerMenuItem()
            {
                Icon = new IconControl() { BindableKind = PackIconMaterialKind.Dns },
                Label = "DNS",
                ToolTip = "View DNS",
                Tag = new DNSListViewModel(Ioc.Default.GetService<SettingsManager>()),
            };

            service.AddMenuItem(volumeTab);
        }

    }

}