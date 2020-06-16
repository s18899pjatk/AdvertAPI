using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class TokenDoesNotExistsException : Exception
    {
        public TokenDoesNotExistsException()
        {
        }

        public TokenDoesNotExistsException(string message) : base(message)
        {
        }
    }
}
