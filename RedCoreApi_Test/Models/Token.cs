using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedCoreApi_Test.Models
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        public string Error { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}