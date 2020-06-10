using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Models.Responses
{
    public class RefreshTokenResponse
    {
        public int IdClient { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string OldRefrshToken { get; set; }
    }
}
