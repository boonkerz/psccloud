using Avalonia.Collections;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PscCloud.Plugin.HetznerServerPlugin.Models
{
    public class Server : ReactiveObject
    {

        private bool isGood;
        private bool hasBackup;

        private String datum;
        private float release;

        private String isGoodIcon = "abacus";

        private String rootDirUsed = "100%";
        private String dataDirUsed = "100%";

        private String mongoVersion = "";

        private int runningContainerCount = 0;

        private String mysqlVersion = "";

        private DateTime backupLastModified;

        private String isGoodColor = "red";

        private string name;

        private AvaloniaList<Domain> domains = new AvaloniaList<Domain>();

        public Server()
        {
            this.hasBackup = false;
            this.isGood = false;
        }

        public bool IsGood
        {
            get => isGood;
            set {
                this.RaiseAndSetIfChanged(ref isGood, value);
                if(value)
                {
                    this.IsGoodIcon = "DatabaseCheck";
                    this.IsGoodColor = "green";
                }
                else
                {
                    this.IsGoodIcon = "DatabaseSync";
                    this.IsGoodColor = "red";
                }
            }
        } 

        public String IsGoodIcon
        {
            get => isGoodIcon;
            set => this.RaiseAndSetIfChanged(ref isGoodIcon, value);
        }

        public String IsGoodColor
        {
            get => isGoodColor;
            set => this.RaiseAndSetIfChanged(ref isGoodColor, value);
        }

        public long Id { get; set; }

        public string Name {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public AvaloniaList<Domain> Domains
        {
            get => domains;
            set => this.RaiseAndSetIfChanged(ref domains, value);
        }

        public string RootDirUsed
        {
            get => rootDirUsed;
            set => this.RaiseAndSetIfChanged(ref rootDirUsed, value);
        }

        public string MysqlVersion
        {
            get => mysqlVersion;
            set => this.RaiseAndSetIfChanged(ref mysqlVersion, value);
        }

        public string MongoVersion
        {
            get => mongoVersion;
            set => this.RaiseAndSetIfChanged(ref mongoVersion, value);
        }

        public int RunningContainerCount
        {
            get => runningContainerCount;
            set => this.RaiseAndSetIfChanged(ref runningContainerCount, value);
        }

        public string DataDirUsed
        {
            get => dataDirUsed;
            set => this.RaiseAndSetIfChanged(ref dataDirUsed, value);
        }

        public string Datum
        {
            get => datum;
            set => this.RaiseAndSetIfChanged(ref datum, value);
        }

        public float Release
        {
            get => release;
            set => this.RaiseAndSetIfChanged(ref release, value);
        }


        public string Ip4 { get; set; }

        public string Ip6 { get; set; }

        public string Status { get; set; }

        public DateTime BackupLastModified {
            get => backupLastModified;
            set => this.RaiseAndSetIfChanged(ref backupLastModified, value);
        }

        public DateTimeOffset Created { get; set; }

        public bool HasBackup {
            get => hasBackup;
            set => this.RaiseAndSetIfChanged(ref hasBackup, value);
        }

    }
}
