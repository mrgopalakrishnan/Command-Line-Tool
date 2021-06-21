using System;
using System.Reflection;

namespace CommandLineTool.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AppAttribute : Attribute
    {
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
        public static BindingFlags DefaultBindingFlags { get; private set; } = BindingFlags.NonPublic
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
            | BindingFlags.Static
            | BindingFlags.Public
            | BindingFlags.Instance
            | BindingFlags.DeclaredOnly;
        public string Description { get; init; }
        public BindingFlags BindingFlags { get; init; } = DefaultBindingFlags;
        public AppAttribute(string description) => Description = description;
        public AppAttribute(BindingFlags bindingFlags) => BindingFlags = bindingFlags;
        public AppAttribute(string description, BindingFlags bindingFlags)
        {
            Description = description;
            BindingFlags = bindingFlags;
        }
    }
}
