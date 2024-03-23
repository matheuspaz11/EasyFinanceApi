using EasyFinanceApi.Models.Entities;

namespace EasyFinanceApi.Mapper
{
    public class ExpenseMapper : BaseMapper<Expense>
    {
        public ExpenseMapper() : base("tb_expense") {}
    }
}
