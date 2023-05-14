namespace BusinessServiceTemplate.Shared.Enumerations
{
    /// <summary>
    /// Used to attribute Enum types, providing additional contextual information for the enumeration value
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumMetadataAttribute : Attribute
    {
        public EnumMetadataAttribute(string name, string description, int order)
        {
            Description = description;
            Name = name;
            Order = order;
        }

        public EnumMetadataAttribute(string name, string description)
        {
            Description = description;
            Name = name;
        }

        public string Name { get; }

        public string Description { get; }

        public int? Order { get; }
    }
}
