namespace BusinessServiceTemplate.Shared.Exceptions
{
    /// <summary>
    /// An exception used to alert to system internal errors.
    /// </summary>
    public class SystemInternalException : Exception
    {
        public SystemInternalException(string message)
            : base(message)
        {
        }

        public SystemInternalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
