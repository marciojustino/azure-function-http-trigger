using System.Text.Json.Serialization;

namespace TOTVS.Tests.Function.Models
{
    public class Link
    {
        [JsonPropertyName("previous")]
        public string Previous { get; set; }

        [JsonPropertyName("current")]
        public string Current { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }
    }
}
