using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PscCloud.Plugin.HetznerVolumePlugin.Models
{
    public class Volume : ReactiveObject
    {
        private string name;


        public Volume()
        {
            this.Protected = "nicht aktiviert";
        }
        
        public long Id { get; set; }

        public string Name {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public DateTimeOffset Created { get; set; }

        public String Size { get; set; }

        public String Protected { get; set; }

    }
}
