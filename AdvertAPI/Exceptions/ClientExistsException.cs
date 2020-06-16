using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class ClientExistsException : Exception
    {
        public ClientExistsException()
        {
        }

        public ClientExistsException(string message) : base(message)
        {
        }

        protected ClientExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
