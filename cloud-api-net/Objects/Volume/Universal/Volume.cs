using System;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Volume.Universal
{
    public class Protection
    {
        public bool delete { get; set; }
    }

    public class Volume
    {
        public long id { get; set; }
        public string name { get; set; }
        public long size { get; set; }
        public Protection protection { get; set; }
        public DateTime created { get; set; }
    }
}
