using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchMvcReact.BL.ElasticSearchAttributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class Suggest : System.Attribute
    {
        public double version;

        public Suggest()
        {
            version = 1.0;
        }
    }
}
