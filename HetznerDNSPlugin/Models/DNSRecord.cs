using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PscCloud.Plugin.HetznerDNSPlugin.Models
{
    public class DNSRecord : ReactiveObject
    {
        private string name;
        private string wert;
        private string type;


        public DNSRecord()
        {
          
        }
        
        public String Id { get; set; }

        public string Name {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public string Wert
        {
            get => wert;
            set => this.RaiseAndSetIfChanged(ref wert, value);
        }

        public string Type
        {
            get => type;
            set => this.RaiseAndSetIfChanged(ref type, value);
        }

        public String Created { get; set; }

        

    }
}
