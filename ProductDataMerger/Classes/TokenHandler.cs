using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProductDataMerger.Interfaces;
using ProductDataMerger.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Classes
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IApiRequest _apiRequest;
        private readonly IConfigurationManager _configuration;
        public TokenHandler(IApiRequest apiRequest, IConfigurationManager configuration)
        {
            _apiRequest = apiRequest;
            _configuration = configuration;
        }

        public async Task<JwtToken?> GetAccessToken(IConfigurationRoot configurationRoot)
        {
            var authenticationConfiguration = _configuration.GetSectionPropertiesInDictionary(configurationRoot, "AuthenticanSettings");
            var parametersConfiguration = _configuration.GetSectionPropertiesInDictionary(configurationRoot, "AuthenticanSettings:Parameters");
            var headersConfiguration = _configuration.GetSectionPropertiesInDictionary(configurationRoot, "AuthenticanSettings:Headers");

            var response = await _apiRequest.PostRequest(authenticationConfiguration["token_base_url"],
                authenticationConfiguration["token_resource_url"], parametersConfiguration, headersConfiguration);

            if (response.IsSuccessful)
            {
                if (!string.IsNullOrEmpty(response.Content))
                {
                    var jwtToken = JsonConvert.DeserializeObject<JwtToken>(response.Content);
                    return jwtToken;
                }
                else
                {
                    return null;
                }
            }

            return null;

        }

        public bool IsTokenValid(JwtToken token)
        {
            if(token is null)
                return false;

            var jwtToken = new JwtSecurityToken(token.AccessToken);

            if(jwtToken.ValidTo > DateTime.UtcNow)
            {
                return true;
            }
            return false;
        }



    }
}
