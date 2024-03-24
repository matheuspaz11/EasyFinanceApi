using static EasyFinanceApi.Models.Enums.Enum;

namespace EasyFinanceApi.Models.Entities
{
    public class Expense : Base
    {
        public string Description { get; set; }

        public long Value { get; set; }

        public ExpenseType Type { get; set; }

        public int Maturity { get; set; }

        public ExpenseStatus Status { get; set; }

        public DateTime? PaymentDate {  get; set; }
    }
}