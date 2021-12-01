using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Collections;
using DynamicData;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Prise;
using PscCloud.Plugin.Contract;
using PscCloud.Shared.Service;
using PscCloud.Shared.Settings;

namespace PscCloud.Service
{
    public class PluginService
    {
        private Dictionary<string, IAppPlugin> loadedPlugins = new Dictionary<string, IAppPlugin>();
        private Dictionary<string, string> foundPlugins = new Dictionary<string, string>();
        
        public PluginService()
        {
            this.scanPlugins();


        }


        public Dictionary<string, string> GetPlugins()
        {
            return this.foundPlugins;
        }

        async private void scanPlugins()
        {
            var pluginLoader = Ioc.Default.GetService<IPluginLoader>();
            var dist = getPluginPath();
            var pluginScanResults = await pluginLoader.FindPlugins<IAppPlugin>(dist);

            var menuService = Ioc.Default.GetService<MenuService>();
            
            //components = new Dictionary<string, IAppComponent>();
            foreach (var pluginScanResult in pluginScanResults)
            {
                this.foundPlugins.Add(Path.GetFileNameWithoutExtension(pluginScanResult.AssemblyName), pluginScanResult.AssemblyPath);
            }
        }
        
        private string getPluginPath()
        {
            var pathToThisProgram = Assembly.GetExecutingAssembly() // this assembly location (/bin/Debug/netcoreapp3.1)
                .Location;
            var pathToExecutingDir = Path.GetDirectoryName(pathToThisProgram);
            return Path.GetFullPath(Path.Combine(pathToExecutingDir, "../../../../_dist"));
        }

        async public void LoadPlugin(string pluginName)
        {
            if(this.loadedPlugins.ContainsKey(pluginName))
            {
                return;
            }

            var pluginLoader = Ioc.Default.GetService<IPluginLoader>();
            var menuService = Ioc.Default.GetService<MenuService>();
            var settingsService = Ioc.Default.GetService<SettingsManager>();
            
            var pluginAssemblies = await pluginLoader.FindPlugins<IAppPlugin>(getPluginPath());
            var pluginToEnable = pluginAssemblies.FirstOrDefault(p => Path.GetFileNameWithoutExtension(p.AssemblyName) == pluginName);

            if (pluginToEnable != null)
            {
                var plugin = await pluginLoader.LoadPlugin<IAppPlugin>(pluginToEnable, configure: (context) =>
                {
                    context
                        .AddHostTypes(new[] {typeof(Application)})
                        .AddHostService<MenuService>(menuService)
                        .AddHostService<SettingsManager>(settingsService)
                        ;
                });


                if (!this.loadedPlugins.ContainsKey(pluginName))
                {
                    loadedPlugins.Add(plugin.GetName(), plugin);
                    plugin.addMenu();
                }
            }
        }
    }
}