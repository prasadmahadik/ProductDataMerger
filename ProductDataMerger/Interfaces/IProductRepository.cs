using Microsoft.Extensions.Configuration;
using ProductDataMerger.Enums;
using ProductDataMerger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductResponse?> GetProducts(IConfigurationRoot configurationRoot, JwtToken token, ProductDataTypeEnum dataType);
        //string ConvertXmlToJson(string jsonContent);
    }
}
