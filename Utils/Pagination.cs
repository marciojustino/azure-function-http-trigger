using System.Text.Json.Serialization;
using TOTVS.Tests.Function.Models;

namespace TOTVS.Tests.Function
{
    public class Pagination
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("pages")]
        public int Pages { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        [JsonPropertyName("links")]
        public Link Links { get; set; }
    }
}
