using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchMvcReact.BL.ElasticSearchAttributes
{
     public enum AnalyzerTypes
    {
        English,
        Standard,
    }

    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class Analyzer : System.Attribute
    {
        public double version;
        public AnalyzerTypes analyzerType;

        public Analyzer()
        {
            version = 1.0;
            analyzerType = AnalyzerTypes.Standard;
        }

        public string ConvertAnalyzerTypeToElasticAnalyzer()
        {
            switch (analyzerType)
            {
                case AnalyzerTypes.English:
                    return "english";
                case AnalyzerTypes.Standard:
                default:
                    return "standard";
            }
        }
    }
}
