using EasyFinanceApi.Helpers.Validators;
using System.Text.Json.Serialization;

namespace EasyFinanceApi.Models.DTOs
{
    public class ObjectiveDTO
    {
        [JsonPropertyName("Description")]
        [StringValidate]
        public string Description { get; set; }

        [JsonPropertyName("Value")]
        public decimal Value { get; set; }

        [JsonPropertyName("CurrentValue")]
        public decimal CurrentValue { get; set; }

        [JsonPropertyName("InvestmentMonth")]
        public decimal InvestmentMonth { get; set; }
    }
}
