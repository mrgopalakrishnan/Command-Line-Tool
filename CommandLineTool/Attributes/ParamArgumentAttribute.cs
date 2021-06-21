namespace CommandLineTool.Attributes
{
    public class ParamArgumentAttribute : ParamAttribute
    {
        public ParamArgumentAttribute()
        {
        }
        public ParamArgumentAttribute(string description) => Description = description;

    }
}
