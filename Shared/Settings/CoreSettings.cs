using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PscCloud.Shared.Settings
{
    public class CoreSettings
    {
        public int ApplicationUpdateFrequencyMinutes { get; set; } = 60;
        
        public List<string> ActivatedPlugins { get; set; }
        public string ActiveMenuItem { get; set; }

        public CoreSettings()
        {
            ActivatedPlugins = new List<string>();
        }
    }
}
