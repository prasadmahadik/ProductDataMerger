using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Interfaces
{
    public interface IConfigurationManager 
    {
        Dictionary<string, string>? GetSectionPropertiesInDictionary(IConfigurationRoot configurationRoot, string section);
    }
}
