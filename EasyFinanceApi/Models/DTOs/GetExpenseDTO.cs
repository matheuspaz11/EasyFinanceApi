using System.Text.Json.Serialization;

namespace EasyFinanceApi.Models.DTOs
{
    public class GetExpenseDTO
    {
        [JsonPropertyName("Descrição")]
        public string Description { get; set; }

        [JsonPropertyName("Valor")]
        public long Value { get; set; }

        [JsonPropertyName("Tipo")]
        public string Type { get; set; }

        [JsonPropertyName("Vencimento")]
        public int Maturity { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("PaymentDate")]
        public DateTime? PaymentDate { get; set; }
    }
}
