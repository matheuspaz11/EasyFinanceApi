using System.ComponentModel;

namespace EasyFinanceApi.Models.Enums
{
    public class Enum
    {
        public enum ExpenseType
        {
            [Description("Fixa")]
            Fixed = 0,
            [Description("Variável")]
            Variable = 1
        }

        public enum ExpenseStatus
        {
            [Description("Pendente")]
            Pending = 0,
            [Description("Paga")]
            Liquidated = 1
        }
    }
}
