using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Prise.Plugin;
using PscCloud.Plugin.Contract;
using PscCloud.Plugin.HetznerVolumePlugin.ViewModels;
using PscCloud.Plugin.HetznerVolumePlugin.Views;

namespace PscCloud.Plugin.HetznerVolumePlugin
{
    [PluginBootstrapper(PluginType = typeof(HetznerVolumePlugin))]
    public class HetznerVolumePluginBootstrap : IPluginBootstrapper
    {
        public IServiceCollection Bootstrap(IServiceCollection services)
        {
            services.AddTransient<VolumeListViewModel>();
            return services;
        }
    }
}