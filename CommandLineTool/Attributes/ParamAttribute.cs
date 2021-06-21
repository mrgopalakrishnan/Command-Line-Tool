using System;

namespace CommandLineTool.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public abstract class ParamAttribute : Attribute
    {
        public string Description { get; init; }

    }
}
