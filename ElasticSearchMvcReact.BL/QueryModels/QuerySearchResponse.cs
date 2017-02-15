﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchMvcReact.BL.QueryModels
{
    public class QuerySearchResponse<T>
    {
        public int took { get; set; }
        public bool timed_out { get; set; }
        public _Shards _shards { get; set; }
        public Hits<T> hits { get; set; }
    }

    public class Hits<T>
    {
        public int total { get; set; }
        public double max_score { get; set; }
        public Hit<T>[] hits { get; set; }
    }

    public class Hit<T>
    {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public double _score { get; set; }
        public T _source { get; set; }
    }
}
