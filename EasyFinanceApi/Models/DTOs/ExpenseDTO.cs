using System.Text.Json.Serialization;

namespace EasyFinanceApi.Models.DTOs
{
    public class ExpenseDTO
    {
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Value")]
        public long Value { get; set; }

        [JsonPropertyName("Type")]
        public long Type { get; set; }

        [JsonPropertyName("Vencimento")]
        public int Maturity { get; set; }
    }
}