using System.Text.Json.Serialization;
using static EasyFinanceApi.Models.Enums.Enum;

namespace EasyFinanceApi.Models.DTOs
{
    public class ChangeExpenseDTO
    {
        [JsonPropertyName("Descrição")]
        public string Description { get; set; }

        [JsonPropertyName("Valor")]
        public long? Value { get; set; }

        [JsonPropertyName("Tipo")]
        public ExpenseType? Type { get; set; }

        [JsonPropertyName("Vencimento")]
        public int? Maturity { get; set; }
    }
}
