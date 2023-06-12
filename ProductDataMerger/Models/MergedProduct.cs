using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Models
{
    public class MergedProduct
    {
        public long BannerUpc { get; set; }
        public string  BannerItemCode { get; set; }
        public long RshughesUpc { get; set; }
        public string RshughesItemCode { get; set; }
    }
}
