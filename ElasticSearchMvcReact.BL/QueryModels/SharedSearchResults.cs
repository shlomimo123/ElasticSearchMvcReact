using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchMvcReact.BL.QueryModels
{
    public class _Shards
    {
        public int total { get; set; }
        public int successful { get; set; }
        public int failed { get; set; }
    }
}
