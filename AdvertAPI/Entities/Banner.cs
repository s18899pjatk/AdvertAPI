using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Entities
{
    public class Banner
    {
        public int IdAdvertisement { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int IdCampaign { get; set; }
        public decimal Area { get; set; }
        public Campaign Campaign { get; set; }
    }
}
