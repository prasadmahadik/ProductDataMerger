using ProductDataMerger.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Classes
{
    public class RestClientFactory : IRestClientFactory
    {
        public IRestClient GetRestClient(string url)
        {
            return new RestClient(url);
        }
    }
}
