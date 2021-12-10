using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PscCloud.Shared.Settings
{
    public class CoreSettings
    {
        public string ApiToken { get; set; }
        public string DNSApiToken { get; set; }
        public string SSHFile { get; set; }
        public int ApplicationUpdateFrequencyMinutes { get; set; } = 60;
        
        public List<string> ActivatedPlugins { get; set; }

        public CoreSettings()
        {
            ActivatedPlugins = new List<string>();
        }
    }
}
