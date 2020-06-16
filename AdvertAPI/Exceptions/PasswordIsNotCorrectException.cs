using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class PasswordIsNotCorrectException : Exception

    {
        public PasswordIsNotCorrectException()
        {
        }

        public PasswordIsNotCorrectException(string message) : base(message)
        {
        }

        public PasswordIsNotCorrectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
