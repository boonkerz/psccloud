using Avalonia.Collections;
using Avalonia.Threading;
using Nager.HetznerDns;
using Nager.HetznerDns.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PscCloud.Plugin.HetznerDNSPlugin.Models;
using PscCloud.Plugin.HetznerDNSPlugin.Views;
using PscCloud.Shared.Models.ViewModels;
using PscCloud.Shared.Service;
using PscCloud.Shared.Settings;

namespace PscCloud.Plugin.HetznerDNSPlugin.ViewModels
{
    class DNSListViewModel : ViewModelBase, IPluginViewModelBase
    {

        public AvaloniaList<DNSRecord> RecordList { get; set; }

        public SettingsManager settings;

        private readonly DispatcherTimer autoSaveTimer = new DispatcherTimer();

        public DNSListViewModel(SettingsManager settingsManager)
        {
            settings = settingsManager;
            RecordList = new AvaloniaList<DNSRecord>();

            Task.Run(async () => await this.reloadVolumes());
           
        }

        private async Task reloadVolumes()
        {
            var client = new HetznerDnsClient(settings.CoreSettings.DNSApiToken);
            Nager.HetznerDns.Models.RecordResponse? response = await client.GetRecordsAsync("tiBfyNXnywMQ6rheboNtCF");

            foreach (Record record in response.Records)
            {
                var rec = new DNSRecord();
                rec.Name = record.Name;
                rec.Id = record.Id;
                rec.Created = record.Created;
                rec.Wert = record.Value;
                rec.Type = record.Type.ToString();
                

                var uiDispatcher = Ioc.Default.GetService<IUserInterfaceDispatchService>();
                await uiDispatcher.InvokeAsync(() =>
                {
                    RecordList.Add(rec);
                });

            }


        }


        public UserControl GetViewControl()
        {
            return new DNSListView();
        }
    }
}
