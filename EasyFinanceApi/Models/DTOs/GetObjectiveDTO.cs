using System.Text.Json.Serialization;

namespace EasyFinanceApi.Models.DTOs
{
    public class GetObjectiveDTO
    {
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Value")]
        public decimal Value { get; set; }

        [JsonPropertyName("CurrentValue")]
        public decimal CurrentValue { get; set; }

        [JsonPropertyName("InvestmentMonth")]
        public decimal InvestmentMonth { get; set; }

        [JsonPropertyName("MonthsToObjective")]
        public int MonthsToObjective { get; set; }

        [JsonPropertyName("LastEntry")]
        public decimal LastEntry { get; set; }

        [JsonPropertyName("CreationDate")]
        public DateTime? CreationDate { get; set; }

        [JsonPropertyName("UpdateDate")]
        public DateTime? UpdateDate { get; set; }
    }
}
