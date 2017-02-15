using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearchMvcReact.BL.ElasticSearchAttributes
{
    public enum FieldTypes
    {
        _long,
        _string,
        _date
    }

    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FieldType : System.Attribute
    {
        public double version;
        public FieldTypes fieldType;

        public FieldType()
        {
            version = 1.0;
            fieldType = FieldTypes._string;
        }

        public string ConvertFieldTypeToElasticFieldType()
        {
            switch (fieldType)
            {
                case FieldTypes._long:
                    return "long";
                case FieldTypes._date:
                    return "date";
                case FieldTypes._string:
                default:
                    return "string";
            }
        }
    }
}
