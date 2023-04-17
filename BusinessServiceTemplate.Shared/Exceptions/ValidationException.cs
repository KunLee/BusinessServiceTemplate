using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceTemplate.Shared.Exceptions
{
    /// <summary>
    /// An exception used to alert to request validation errors.  It is assumed that exceptions of this
    /// nature are human-readable and safe to report back to the end user.
    /// </summary>
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
