using System.Text.Json.Serialization;
using static EasyFinanceApi.Models.Enums.Enum;

namespace EasyFinanceApi.Models.DTOs
{
    public class ChangeExpenseDTO
    {
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Value")]
        public decimal? Value { get; set; }

        [JsonPropertyName("Type")]
        public ExpenseType? Type { get; set; }

        [JsonPropertyName("Maturity")]
        public int? Maturity { get; set; }
    }
}
