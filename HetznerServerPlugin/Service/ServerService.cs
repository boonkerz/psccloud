using Avalonia.Collections;
using Avalonia.Threading;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PscCloud.Plugin.HetznerServerPlugin.Models;
using PscCloud.Shared.Service;
using PscCloud.Shared.Settings;

namespace PscCloud.Plugin.HetznerServerPlugin.Service
{
    public class ServerService: ReactiveObject
    {
        private String dfCommand = @"df -k --portability | sed -e ""s/\s\{1,\}/,/g"" | awk 'BEGIN{FS="","";json = ""[{""}{if (NR == 1) { for (i=1;i < NF+1;i++) { key[i] = $i}};if(NR != 1){for (i=1;i < NF;i++) { json = json ""\"""" key[i] ""\"":\"""" $i ""\"",""}; json = json ""\"""" key[NF] ""\"":\"""" $NF ""\""},{"" }} END {sub(/,\{$/, """", json); json = json ""]""; print json}'";

        public AvaloniaList<Server> ServerList { get; set; }

        public SettingsManager settings;

        private readonly DispatcherTimer autoSaveTimer = new DispatcherTimer();

        private static readonly HttpClient httpClient = new HttpClient();

        public event EventHandler SyncStarted;
        public event EventHandler SyncCompleted;
        private bool isOnRefreshStatus = false;
        private bool isOnDockerPrune = false;

        public ServerService()
        {
        }

        public void Init()
        {
            settings = Ioc.Default.GetService<SettingsManager>();
            ServerList = new AvaloniaList<Server>();
            lkcode.hetznercloudapi.Core.ApiCore.ApiToken = settings.CoreSettings.ApiToken;

            autoSaveTimer.Tick += new EventHandler(reloadServerStatus);
            autoSaveTimer.Interval = new TimeSpan(0, settings.CoreSettings.ApplicationUpdateFrequencyMinutes, 0);
            autoSaveTimer.Start();

            Task.Run(async () => await this.reloadServer());

            this.reloadServer();
        }

        public void RefreshStatus()
        {
            if (!this.isOnDockerPrune && !this.isOnRefreshStatus)
            {
                Task.Run(async () => await this.reloadServerStatus());
            }
        }

        public void DockerPruneAllServer()
        {
            if (!this.isOnDockerPrune && !this.isOnRefreshStatus)
            {
                Task.Run(async () => await this.dockerPruneAllServer());
            }
        }

        private void reloadServerStatus(object sender, EventArgs e)
        {
            Task.Run(async () => await this.reloadServerStatus());
        }

        protected virtual void OnSyncStarted(EventArgs e)
        {
            SyncStarted?.Invoke(this, e);
        }

        protected virtual void OnSyncCompleted(EventArgs e)
        {
            SyncCompleted?.Invoke(this, e);
        }

        private async Task reloadServer()
        {
            OnSyncStarted(EventArgs.Empty); //No event data
            var servers = await lkcode.hetznercloudapi.Api.Server.GetAsync(1);

            foreach (lkcode.hetznercloudapi.Api.Server server in servers)
            {
                var serv = new Server();
                serv.Name = server.Name;
                serv.Id = server.Id;
                serv.Status = server.Status;
                serv.Created = server.Created;
                serv.Ip4 = server.Network.Ipv4.Ip.ToString();
                serv.Ip6 = server.Network.Ipv6.Ip.ToString();


                var uiDispatcher = Ioc.Default.GetService<IUserInterfaceDispatchService>();
                await uiDispatcher.InvokeAsync(() =>
                {
                    ServerList.Add(serv);
                });

            }

            if (servers.Capacity > 25)
            {
                servers = await lkcode.hetznercloudapi.Api.Server.GetAsync(2);

                foreach (lkcode.hetznercloudapi.Api.Server server in servers)
                {
                    var serv = new Server();
                    serv.Name = server.Name;
                    serv.Id = server.Id;
                    serv.Status = server.Status;
                    serv.Created = server.Created;
                    serv.Ip4 = server.Network.Ipv4.Ip.ToString();
                    serv.Ip6 = server.Network.Ipv6.Ip.ToString();


                    var uiDispatcher = Ioc.Default.GetService<IUserInterfaceDispatchService>();
                    await uiDispatcher.InvokeAsync(() =>
                    {
                        ServerList.Add(serv);
                    });

                }
            }
            OnSyncCompleted(EventArgs.Empty); //No event data

            Task.Run(async () => await this.reloadServerStatus());

        }

        private async Task reloadServerStatus()
        {
            this.isOnRefreshStatus = true;
            OnSyncStarted(EventArgs.Empty); //No event data

            var uiDispatcher = Ioc.Default.GetService<IUserInterfaceDispatchService>();

            await uiDispatcher.InvokeAsync(() =>
            {
                this.autoSaveTimer.Stop();
            });

            foreach (Server serv in ServerList)
            {

                serv.IsGood = false;

                var connectionInfo = new ConnectionInfo(serv.Ip4,
                                            "root",
                                            new PrivateKeyAuthenticationMethod("root", new PrivateKeyFile(settings.CoreSettings.SSHFile)));
                using (var client = new SshClient(connectionInfo))
                {
                    try
                    {
                        client.Connect();
                        // var command = client.RunCommand("apt install borgmagic jq -y");
                        var command = client.RunCommand("borgmatic info --json");
                        if (command.ExitStatus == 0)
                        {
                            serv.HasBackup = true;
                            JArray borgMatic = JArray.Parse(command.Result);
                            await uiDispatcher.InvokeAsync(() =>
                            {
                                serv.BackupLastModified = DateTime.Parse(borgMatic[0]["repository"]["last_modified"].ToString());
                            });

                        }
                        command = client.RunCommand(dfCommand);
                        if (command.ExitStatus == 0)
                        {
                            serv.HasBackup = true;
                            JArray borgMatic = JArray.Parse(command.Result);

                            foreach (var entry in borgMatic)
                            {
                                if (entry["Mounted"].ToString() == "/")
                                {
                                    serv.RootDirUsed = entry["Capacity"].ToString();
                                }

                                if (entry["Mounted"].ToString() == "/data")
                                {
                                    serv.DataDirUsed = entry["Capacity"].ToString();
                                }
                            }
                        }
                        command = client.RunCommand("docker inspect psc_mongodb_1");
                        if (command.ExitStatus == 0)
                        {
                            JArray mongoDB = JArray.Parse(command.Result);
                            var mongoDBEnvArray = mongoDB[0]["Config"]["Env"];

                            foreach (String entry in mongoDBEnvArray)
                            {
                                var mongodDBexploded = entry.Split("=");
                                if (mongodDBexploded[0] == "MONGO_VERSION")
                                {
                                    serv.MongoVersion = mongodDBexploded[1];
                                }
                            }
                        }

                        command = client.RunCommand("docker inspect psc_mysql_1");
                        if (command.ExitStatus == 0)
                        {
                            JArray mongoDB = JArray.Parse(command.Result);
                            var mongoDBEnvArray = mongoDB[0]["Config"]["Env"];

                            foreach (String entry in mongoDBEnvArray)
                            {
                                var mongodDBexploded = entry.Split("=");
                                if (mongodDBexploded[0] == "MARIADB_VERSION")
                                {
                                    serv.MysqlVersion = mongodDBexploded[1];
                                }
                            }
                        }

                        command = client.RunCommand("docker ps|wc -l");
                        if (command.ExitStatus == 0)
                        {
                            serv.RunningContainerCount = int.Parse(command.Result);
                        }

                        command = client.RunCommand("docker inspect psc_web_1");
                        if (command.ExitStatus == 0)
                        {
                            JArray web = JArray.Parse(command.Result);
                            var webEnvArray = web[0]["Config"]["Env"];

                            foreach (String entry in webEnvArray)
                            {
                                var webExploded = entry.Split("=");
                                if (webExploded[0] == "LETSENCRYPT_HOST")
                                {
                                    var webHosts = webExploded[1].Split(",");
                                    serv.Domains.Clear();
                                    foreach (String dom in webHosts)
                                    {
                                        serv.Domains.Add(new Domain(dom));
                                    }

                                    httpClient.DefaultRequestHeaders.Accept.Clear();
                                    httpClient.DefaultRequestHeaders.Accept.Add(
                                        new MediaTypeWithQualityHeaderValue("application/ld+json"));
                                    httpClient.DefaultRequestHeaders.Add("User-Agent", "PSC Client");

                                    var stringTask = httpClient.GetStringAsync("https://" + webHosts[0] + "/apps/api/system/version");

                                    var msg = await stringTask;
                                    JObject versionMSg = JObject.Parse(msg);

                                    serv.Datum = versionMSg["datum"].ToString();
                                    serv.Release = float.Parse(versionMSg["release"].ToString());

                                    
                                }
                            }
                        }

                        client.Disconnect();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    serv.IsGood = true;
                }

            }

            await uiDispatcher.InvokeAsync(() =>
            {
                this.autoSaveTimer.Start();
            });

            OnSyncCompleted(EventArgs.Empty); //No event data
            this.isOnRefreshStatus = false;
        }

        public async void GetMetric(Server server)
        {
            lkcode.hetznercloudapi.Api.Server serverObj = await lkcode.hetznercloudapi.Api.Server.GetByIdAsync(server.Id);

            //lkcode.hetznercloudapi.Api.ServerMetric serverMetric = await serverObj.GetMetrics("cpu,disk,network", "2021-07-29T00:00:00Z", "2021-07-29T20:56:00Z");
        }

        private async Task dockerPruneAllServer()
        {
            this.isOnDockerPrune = true;
            OnSyncStarted(EventArgs.Empty); //No event data

            var uiDispatcher = Ioc.Default.GetService<IUserInterfaceDispatchService>();

            await uiDispatcher.InvokeAsync(() =>
            {
                this.autoSaveTimer.Stop();
            });

            foreach (Server serv in ServerList)
            {

                serv.IsGood = false;

                var connectionInfo = new ConnectionInfo(serv.Ip4,
                                            "root",
                                            new PrivateKeyAuthenticationMethod("root", new PrivateKeyFile(settings.CoreSettings.SSHFile)));
                using (var client = new SshClient(connectionInfo))
                {
                    try
                    {
                        client.Connect();
                        client.RunCommand("docker system prune -f");
                        var command = client.RunCommand(dfCommand);
                        if (command.ExitStatus == 0)
                        {
                            JArray borgMatic = JArray.Parse(command.Result);

                            foreach (var entry in borgMatic)
                            {
                                if (entry["Mounted"].ToString() == "/")
                                {
                                    serv.RootDirUsed = entry["Capacity"].ToString();
                                }

                                if (entry["Mounted"].ToString() == "/data")
                                {
                                    serv.DataDirUsed = entry["Capacity"].ToString();
                                }
                            }
                        }
                        serv.IsGood = true;
                        client.Disconnect();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            await uiDispatcher.InvokeAsync(() =>
            {
                this.autoSaveTimer.Start();
            });

            this.isOnDockerPrune = false;
            OnSyncCompleted(EventArgs.Empty); //No event data
        }
    }
}
