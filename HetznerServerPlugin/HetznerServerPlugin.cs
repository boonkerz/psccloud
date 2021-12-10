using Avalonia.Controls;
using MicroCubeAvalonia.Controls;
using MicroCubeAvalonia.IconPack;
using MicroCubeAvalonia.IconPack.Icons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Prise.Plugin;
using PscCloud.Plugin.Contract;
using PscCloud.Plugin.HetznerServerPlugin.ViewModels;
using PscCloud.Service;

namespace PscCloud.Plugin.HetznerServerPlugin
{

    [Plugin(PluginType = typeof(IAppPlugin))]
    public class HetznerServerPlugin : IAppPlugin
    {
        public string GetName()
        {
            return "HetznerServerPlugin";
        }

        public void addMenu()
        {
            MenuService service = Ioc.Default.GetService<MenuService>();
            
            var volumeTab = new HamburgerMenuItem()
            {
                Icon = new IconControl() { BindableKind = PackIconMaterialKind.Server },
                Label = "Server",
                ToolTip = "View Servers",
                Tag = new ServerListViewModel()
            };

            service.AddMenuItem(volumeTab);
        }
    }

}