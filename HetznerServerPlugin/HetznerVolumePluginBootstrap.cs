using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Prise.Plugin;
using PscCloud.Plugin.Contract;
using PscCloud.Plugin.HetznerServerPlugin.Service;
using PscCloud.Plugin.HetznerServerPlugin.ViewModels;

namespace PscCloud.Plugin.HetznerServerPlugin
{
    [PluginBootstrapper(PluginType = typeof(HetznerServerPlugin))]
    public class HetznerServerPluginBootstrap : IPluginBootstrapper
    {
        public IServiceCollection Bootstrap(IServiceCollection services)
        {
           // services.AddTransient<ServerListViewModel>();
            return services;
        }
    }
}