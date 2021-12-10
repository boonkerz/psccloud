using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Prise.Plugin;
using PscCloud.Plugin.Contract;
using PscCloud.Plugin.HetznerDNSPlugin.ViewModels;

namespace PscCloud.Plugin.HetznerDNSPlugin
{
    [PluginBootstrapper(PluginType = typeof(HetznerDNSPlugin))]
    public class HetznerDMSPluginBootstrap : IPluginBootstrapper
    {
        public IServiceCollection Bootstrap(IServiceCollection services)
        {
            services.AddTransient<DNSListViewModel>();
            return services;
        }
    }
}