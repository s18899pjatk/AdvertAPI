using AdvertAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Models.Responses
{
    public class CampaignListResponse
    {
        public int IdClient { get; set; }
        public string LastName { get; set; }
        public List<int> Campaigns { get; set; }
        public List<List<string>> Banners { get; set; }

    }
}
