namespace CommandLineTool.Attributes
{
    public class ParamOptionAttribute : ParamAttribute
    {
        public string[] Aliases { get; init; }

        public bool IsMandatory { get; init; } = true;

        public ParamOptionAttribute(
            string[] aliases, string description)
        {
            Aliases = aliases;
            Description = description;
        }
        public ParamOptionAttribute(string[] aliases) => Aliases = aliases;
        public ParamOptionAttribute(string[] aliases, string description, bool ismandatory)
        {
            Aliases = aliases;
            Description = description;
            IsMandatory = ismandatory;
        }
        public ParamOptionAttribute(string[] aliases, bool ismandatory)
        {
            Aliases = aliases;
            IsMandatory = ismandatory;
        }
        public ParamOptionAttribute(string aliases, string description)
        {
            Aliases = new[] { aliases };
            Description = description;
        }
        public ParamOptionAttribute(string aliases, string description, bool ismandatory)
        {
            Aliases = new[] { aliases };
            Description = description;
            IsMandatory = ismandatory;
        }

        public ParamOptionAttribute(string aliases) => Aliases = new[] { aliases };
        public ParamOptionAttribute(string aliases, bool ismandatory)
        {
            Aliases = new[] { aliases };
            IsMandatory = ismandatory;
        }
    }
}
