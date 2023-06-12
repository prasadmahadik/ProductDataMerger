using ProductDataMerger.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Classes
{
    public class RestRequestFactory : IRestRequestFactory
    {
        public RestRequest GetRestRequest(string resource, Method method)
        {
            return new RestRequest(resource, method);
        }
    }
}
