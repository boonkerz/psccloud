using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Prise.Plugin;
using PscCloud.Plugin.Contract;

namespace PscCloud.Plugin.Nextcloud.Notes
{
    [PluginBootstrapper(PluginType = typeof(NextcloudNotesPlugin))]
    public class NextcloudNotesPluginBootstrap : IPluginBootstrapper
    {
        public IServiceCollection Bootstrap(IServiceCollection services)
        {
            //services.AddTransient<VolumeListViewModel>();
            return services;
        }
    }
}