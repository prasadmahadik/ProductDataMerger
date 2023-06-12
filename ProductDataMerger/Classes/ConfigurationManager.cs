using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ProductDataMerger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Classes
{
    public class ConfigurationManager : IConfigurationManager
    {
        public Dictionary<string, string>? GetSectionPropertiesInDictionary(IConfigurationRoot configurationRoot, string section)
        {
            Dictionary<string, string>? sectionProperties = new Dictionary<string, string>();

            try
            {
                if (configurationRoot.GetSection(section).Exists())
                {
                    sectionProperties = configurationRoot.GetSection(section).GetChildren().ToDictionary(x => x.Key, x => x.Value);
                    return sectionProperties;
                }
                else
                {
                    return new Dictionary<string, string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred in GetSectionPropertiesInDictionary");
                Console.WriteLine(ex.ToString());
                return new Dictionary<string, string>();
            }
        }
    }
}
