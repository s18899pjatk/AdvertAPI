 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class LoginDoesNotExistsException : Exception
    {
        public LoginDoesNotExistsException()
        {
        }

        public LoginDoesNotExistsException(string message) : base(message)
        {
        }

        public LoginDoesNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
