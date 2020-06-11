using AdvertAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Models.Responses
{
    public class CreateCampaignResponse
    {
        public int IdCampaign { get; set; }
        public decimal TotalPrice { get; set; }
        public List<int> Banners { get; set; }
    }
}
