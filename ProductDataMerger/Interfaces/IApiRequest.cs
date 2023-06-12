using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Interfaces
{
    public interface IApiRequest
    {
        Task<RestResponse> GetRequest(string baseUrl, string resource,
            Dictionary<string, string>? parameterMappings = null, Dictionary<string, string>? headerMappings = null);

        Task<RestResponse> PostRequest(string baseUrl, string resource,
            Dictionary<string, string>? parameterMappings = null, Dictionary<string, string>? headerMappings = null);
    }
}
