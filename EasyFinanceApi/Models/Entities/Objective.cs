namespace EasyFinanceApi.Models.Entities
{
    public class Objective : Base
    {
        public string Description { get; set; }

        public decimal Value { get; set; }

        public decimal CurrentValue { get; set; }

        public decimal InvestmentMonth { get; set; }

        public int MonthsToObjective { get; set; }

        public decimal LastEntry {  get; set; }
    }
}