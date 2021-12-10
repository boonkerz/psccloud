using System;
using System.Collections.Generic;
using System.Text;

namespace PscCloud.Plugin.HetznerServerPlugin.Models
{
    public class Domain
    {
        public string Name { get; set; }

        public Domain(String name)
        {
            this.Name = name;
        }
    }
}
