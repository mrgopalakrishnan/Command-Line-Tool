using System;

namespace CommandLineTool.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public CommandAttribute(string name) => Name = name;
        public CommandAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
