using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PscCloud.Service;
using PscCloud.Shared.Settings;

namespace PscCloud.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnClosing(CancelEventArgs e)
        {

            var pluginService = Ioc.Default.GetService<PluginService>();
            var configService = Ioc.Default.GetService<SettingsManager>();
            configService.CoreSettings.ActivatedPlugins.Clear();
            
            foreach (var plugin in pluginService.GetPlugins())
            {
                if (!configService.CoreSettings.ActivatedPlugins.Contains(plugin.Key))
                {
                    configService.CoreSettings.ActivatedPlugins.Add(plugin.Key);
                }
            }
            
            configService.SaveSettings();
            
            base.OnClosing(e);
        }

        async protected override void OnOpened(EventArgs e)
        {

            var pluginService = Ioc.Default.GetService<PluginService>();
            var configService = Ioc.Default.GetService<SettingsManager>();
            
            foreach (var plugin in configService.CoreSettings.ActivatedPlugins)
            {
                pluginService.LoadPlugin(plugin);
            }

            base.OnOpened(e);

        }
    }
}
