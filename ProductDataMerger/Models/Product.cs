using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Models
{
    public class Product
    {
        [JsonProperty(PropertyName = "itemCode")]
        public string ItemCode { get; set; }

        [JsonProperty(PropertyName = "upc")]
        public long? Upc { get; set; }

    }
}
