namespace ElasticSearchMvcReact.BL.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class SingletonAttribute : System.Attribute
    {
        public double version;

        public SingletonAttribute()
        {
            version = 1.0;
        }
    }
}
