using System;
using System.Collections.Generic;
using System.Text;

namespace PscCloud.Plugin.HetznerServerPlugin.Models
{
    public class TabItem
    {
        public string Header { get; }
        public Server ServerItem { get; }

        public TabItem(string header, Server server)
        {
            Header = header;
            ServerItem = server;
        }

       
    }
}
