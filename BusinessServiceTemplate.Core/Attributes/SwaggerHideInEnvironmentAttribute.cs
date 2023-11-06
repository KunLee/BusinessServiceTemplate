namespace BusinessServiceTemplate.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class SwaggerHideInEnvironmentAttribute : Attribute
    {
        public string[] Environments { get; }

        public SwaggerHideInEnvironmentAttribute(params string[] environments)
        {
            Environments = environments;
        }
    }
}
