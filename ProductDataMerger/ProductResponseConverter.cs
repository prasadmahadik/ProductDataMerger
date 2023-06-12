using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ProductDataMerger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger
{
    public class ProductResponseConverter:  JsonConverter<ProductResponse>
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;
        public override ProductResponse? ReadJson(JsonReader reader, Type objectType, ProductResponse? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            var productJsonResponse = new ProductResponse();
            Product[]? products = null;
            string? content = "";
            if(jObject is not null)
            {
                if (jObject.ContainsKey("ProductsResponse"))
                {
                    JObject parsedJObject = JObject.Parse(jObject["ProductsResponse"].ToString());
                    content = parsedJObject["product"].ToString();
                }
                else if (jObject.ContainsKey("products"))
                {
                    content = jObject["products"].ToString();
                }
            }
            
            if(!string.IsNullOrEmpty(content))
            {
                products = JsonConvert.DeserializeObject<Product[]>(content);
                productJsonResponse.Products = products;
            }
            
            return productJsonResponse;
        }

        public override void WriteJson(JsonWriter writer, ProductResponse? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
