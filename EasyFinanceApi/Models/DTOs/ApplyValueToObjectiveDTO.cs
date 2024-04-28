using System.Text.Json.Serialization;

namespace EasyFinanceApi.Models.DTOs
{
    public class ApplyValueToObjectiveDTO
    {
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Value")]
        public decimal Value { get; set; }
    }
}