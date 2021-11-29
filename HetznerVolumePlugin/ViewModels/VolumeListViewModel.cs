using Avalonia.Collections;
using Avalonia.Threading;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PscCloud.Plugin.HetznerVolumePlugin.Models;
using PscCloud.Plugin.HetznerVolumePlugin.Views;
using PscCloud.Shared.Models.ViewModels;
using PscCloud.Shared.Service;
using PscCloud.Shared.Settings;

namespace PscCloud.Plugin.HetznerVolumePlugin.ViewModels
{
    class VolumeListViewModel : ViewModelBase, IPluginViewModelBase
    {
        public AvaloniaList<Volume> VolumeList { get; set; }

        public SettingsManager settings;

        private readonly DispatcherTimer autoSaveTimer = new DispatcherTimer();

        public VolumeListViewModel(SettingsManager settingsManager)
        {
            settings = settingsManager;
            lkcode.hetznercloudapi.Core.ApiCore.ApiToken = settings.CoreSettings.ApiToken;
            VolumeList = new AvaloniaList<Volume>();

            Task.Run(async () => await this.reloadVolumes());
           
        }

        private async Task reloadVolumes()
        {
            var volumes = await lkcode.hetznercloudapi.Api.Volume.GetAsync();

            foreach (lkcode.hetznercloudapi.Api.Volume volume in volumes)
            {
                var vol = new Volume();
                vol.Name = volume.Name;
                vol.Id = volume.Id;
                vol.Size = volume.Size.ToString() + " GB";
                vol.Created = volume.Created;
                if(volume.Protection.Delete) {
                    vol.Protected = "aktiviert";
                }
                

                var uiDispatcher = Ioc.Default.GetService<IUserInterfaceDispatchService>();
                await uiDispatcher.InvokeAsync(() =>
                {
                    VolumeList.Add(vol);
                });

            }


        }

        public UserControl GetViewControl()
        {
            return new VolumeListView();
        }
    }
}
