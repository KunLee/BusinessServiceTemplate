namespace BusinessServiceTemplate.Shared.Exceptions
{
    /// <summary>
    /// An exception to alert that an attempt was made to access something, where the current security context is not authorised to do so
    /// </summary>
    public class AuthorisationException : Exception
    {
        public AuthorisationException(string message)
            : base(message)
        {
        }

        public AuthorisationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
