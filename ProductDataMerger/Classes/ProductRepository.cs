using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductDataMerger.Enums;
using ProductDataMerger.Interfaces;
using ProductDataMerger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductDataMerger.Classes
{
    public class ProductRepository : IProductRepository
    {
        private readonly IApiRequest _apiRequest;
        private readonly IConfigurationManager _configurationManager;
        public ProductRepository(IApiRequest apiRequest, IConfigurationManager configurationManager)
        {
            _apiRequest = apiRequest;
            _configurationManager = configurationManager;
        }
        public async Task<ProductResponse?> GetProducts(IConfigurationRoot configurationRoot, JwtToken token, ProductDataTypeEnum dataType)
        {
            try
            {
                var productSettings = _configurationManager.GetSectionPropertiesInDictionary(configurationRoot, $"Product{dataType.ToString()}Settings");
                var productHeadersSettings = _configurationManager.GetSectionPropertiesInDictionary(configurationRoot, $"Product{dataType.ToString()}Settings:Headers");
                productHeadersSettings["Authorization"] = productHeadersSettings["Authorization"] + token.AccessToken;

                if (productSettings.Count == 0 || productHeadersSettings.Count == 0)
                {
                    Console.WriteLine("Error fetching the settings. Please check appsettings.json");
                }

                var response = await _apiRequest.GetRequest(productSettings["base_url"], productSettings["resource_url"], null, productHeadersSettings);

                if (response is not null && response.IsSuccessful)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        var content = response.Content;
                        if (dataType == ProductDataTypeEnum.Xml)
                        {
                            content = ConvertXmlToJson(response.Content);
                        }

                        var productResponse = JsonConvert.DeserializeObject<ProductResponse>(content);
                        return productResponse;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred in GetProducts");
                Console.WriteLine(ex.ToString());
                return null;
            }
            
        }

        private string ConvertXmlToJson(string jsonContent)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(jsonContent);
            var content = JsonConvert.SerializeXmlNode(xmlDocument);
            return content;
        }
    }
}
