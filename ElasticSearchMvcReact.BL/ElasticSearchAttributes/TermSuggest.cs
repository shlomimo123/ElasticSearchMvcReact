using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchMvcReact.BL.ElasticSearchAttributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class TermSuggest : System.Attribute
    {
        public double version;

        public TermSuggest()
        {
            version = 1.0;
        }
    }
}
