using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDataMerger.Models
{
    public class JwtToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonIgnore]
        public DateTime TokenAssignedDateTime { get; set; } = DateTime.UtcNow;
    }
}
