using System.Text.Json.Serialization;

namespace EasyFinanceApi.Models.DTOs
{
    public class GetExpenseDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Value")]
        public decimal Value { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Maturity")]
        public int Maturity { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("PaymentDate")]
        public DateTime? PaymentDate { get; set; }

        [JsonPropertyName("CreationDate")]
        public DateTime? CreationDate { get; set; }

        [JsonPropertyName("UpdateDate")]
        public DateTime? UpdateDate { get; set; }
    }
}
