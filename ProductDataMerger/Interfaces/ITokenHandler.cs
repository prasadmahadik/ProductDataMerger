using Microsoft.Extensions.Configuration;
using ProductDataMerger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Interfaces
{
    public interface ITokenHandler
    {
        Task<JwtToken?> GetAccessToken(IConfigurationRoot configurationRoot);
        bool IsTokenValid(JwtToken token);
    }
}
