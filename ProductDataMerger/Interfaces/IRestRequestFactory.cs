using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Interfaces
{
    public interface IRestRequestFactory
    {
        RestRequest GetRestRequest(string resource, Method method);
    }
}
