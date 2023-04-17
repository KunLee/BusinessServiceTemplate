namespace BusinessServiceTemplate.Shared.Exceptions
{
    /// <summary>
    /// Used to signify that the request was badly formatted.  It is assumed that exceptions of this nature are humar-readable
    /// and safe to report back to the end user
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
