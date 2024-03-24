using System.Text.Json.Serialization;
using static EasyFinanceApi.Models.Enums.Enum;

namespace EasyFinanceApi.Models.DTOs
{
    public class ExpenseDTO
    {
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Value")]
        public long Value { get; set; }

        [JsonPropertyName("Type")]
        public ExpenseType Type { get; set; }

        [JsonPropertyName("Vencimento")]
        public int Maturity { get; set; }
    }
}