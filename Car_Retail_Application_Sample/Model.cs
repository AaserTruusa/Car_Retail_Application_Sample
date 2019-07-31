using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetailApp_Sample
{
    struct Model
    {
        public int ModelID { get; set; }
        public string Name { get; set; }
        public double RegularPrice { get; set; }
        public double CampaignPrice { get; set; }
    }
}
