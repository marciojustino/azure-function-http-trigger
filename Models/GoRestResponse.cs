using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TOTVS.Tests.Function.Models
{
    public class GoRestResponse<T> where T : IModel
    {
       [JsonPropertyName("meta")]
        public MetaResponse Meta { get; set; }

        [JsonPropertyName("data")]
        public List<T> Data { get; set; }
    }
}
