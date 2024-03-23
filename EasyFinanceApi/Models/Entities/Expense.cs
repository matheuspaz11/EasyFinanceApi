using System.Text.Json.Serialization;

namespace EasyFinanceApi.Models.Entities
{
    public class Expense : Base
    {
        public string Description { get; set; }

        public long Value { get; set; }

        public long Type { get; set; }

        public int Maturity { get; set; }

        public long Status { get; set; }

        public DateTime PaymentDate {  get; set; }
    }
}