using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Entities
{
    public class Building
    {
        public int IdBuilding { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public decimal Height { get; set; }
        public ICollection<Campaign> FromCampaigns { get; set; }
        public ICollection<Campaign> ToCampaigns { get; set; }

    }
}
