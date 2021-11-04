using System.Text.Json.Serialization;

namespace TOTVS.Tests.Function.Models
{
    public class MetaResponse
    {
        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }
    }
}
