using ProductDataMerger.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Classes
{
    public class ApiRequest : IApiRequest
    {
        private readonly IRestClientFactory restClientFactory;
        private readonly IRestRequestFactory restRequestFactory;
        public ApiRequest(IRestClientFactory restClientFactory, IRestRequestFactory restRequestFactory)
        {
            this.restClientFactory = restClientFactory;
            this.restRequestFactory = restRequestFactory;
        }
        public async Task<RestResponse?> GetRequest(string baseUrl, string resource,
            Dictionary<string, string>? parameterMappings = null, Dictionary<string, string>? headerMappings = null)
        {
            try
            {
                var client = restClientFactory.GetRestClient(baseUrl);
                var request = restRequestFactory.GetRestRequest(resource, Method.Get);
                if (headerMappings != null)
                {
                    foreach (var headerMapping in headerMappings)
                    {
                        request.AddHeader(headerMapping.Key, headerMapping.Value);
                    }
                }

                if (parameterMappings != null)
                {
                    foreach (var parameterMapping in parameterMappings)
                    {
                        request.AddParameter(parameterMapping.Key, parameterMapping.Value);
                    }
                }

                RestResponse response = await client.ExecuteAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred in ApiRequest");
                Console.WriteLine(ex.ToString());
                return null;
            }
            
        }

        public async Task<RestResponse?> PostRequest(string baseUrl, string resource,
            Dictionary<string, string>? parameterMappings = null, Dictionary<string, string>? headerMappings = null)
        {
            try
            {
                var client = restClientFactory.GetRestClient(baseUrl);
                var request = restRequestFactory.GetRestRequest(resource, Method.Post);
                if (headerMappings != null)
                {
                    foreach (var headerMapping in headerMappings)
                    {
                        request.AddHeader(headerMapping.Key, headerMapping.Value);
                    }
                }

                if (parameterMappings != null)
                {
                    foreach (var parameterMapping in parameterMappings)
                    {
                        request.AddParameter(parameterMapping.Key, parameterMapping.Value);
                    }
                }

                RestResponse response = await client.ExecuteAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred in ApiRequest");
                Console.WriteLine(ex.ToString());
                return null;
            }
            
        }
    }
}
