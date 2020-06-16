using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class BuildingsAreNotOnTheSameStreetException : Exception
    {
        public BuildingsAreNotOnTheSameStreetException()
        {
        }

        public BuildingsAreNotOnTheSameStreetException(string message) : base(message)
        {
        }

        public BuildingsAreNotOnTheSameStreetException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
