using lkcode.hetznercloudapi.Objects.Universal;
using System.Collections.Generic;

namespace lkcode.hetznercloudapi.Objects.Volume.Get
{

    public class Response
    {
        public List<Volume.Universal.Volume> volumes { get; set; }
        public Meta meta { get; set; }
    }
}
