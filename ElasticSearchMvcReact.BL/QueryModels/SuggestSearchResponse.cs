using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchMvcReact.BL.QueryModels
{
    public class SuggestSearchResponse
    {
        //public _Shards _shards { get; set; }
        public Dictionary<string,dynamic> mysuggestion { get; set; }
    }

    public class MySuggestion
    {
        public string text { get; set; }
        public int offset { get; set; }
        public int length { get; set; }
        public Option[] options { get; set; }
    }

    public class Option
    {
        public string text { get; set; }
        public double score { get; set; }
        public int freq { get; set; }
    }

}
