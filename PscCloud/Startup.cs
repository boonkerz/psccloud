using PscCloud.Service;
using PscCloud.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Prise.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PscCloud.Shared.Service;
using PscCloud.Shared.Settings;

namespace PscCloud
{
    class Startup
    {
        
        public static void RegisterServices(StartupParameters startupParameters)
        {

            var set = new SettingsManager(startupParameters.SettingsFilePath);
            var menuService = new MenuService();
            
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<IUserInterfaceDispatchService, AvaloniaDispatcherService>()
                    .AddPrise()
                    .AddSingleton<MenuService>(menuService)
                    .AddSingleton<PluginService>()
                    .AddSingleton<SettingsManager>(set)
                    .AddTransient<SettingsViewModel>()
                    .AddTransient<PluginListViewModel>()
                    .BuildServiceProvider());
                
        }
        
        public class StartupParameters
        {
            /// <summary>
            /// Gets or sets the location of the application settings file.
            /// </summary>
            public string SettingsFilePath { get; set; }

        }
    }
    
}
