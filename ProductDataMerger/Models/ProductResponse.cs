using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Models
{
    [JsonConverter(typeof(ProductResponseConverter))]
    public class ProductResponse
    {
        public Product[] Products { get; set; }

    }
}
