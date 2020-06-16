using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class WrongDateException : Exception
    {
        public WrongDateException()
        {
        }

        public WrongDateException(string message) : base(message)
        {
        }
    }
}
