using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class ClientDoesNotExsitsException : Exception
    {
        public ClientDoesNotExsitsException()
        {
        }

        public ClientDoesNotExsitsException(string message) : base(message)
        {
        }
    }
}
