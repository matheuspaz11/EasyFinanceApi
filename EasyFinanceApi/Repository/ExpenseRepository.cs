using EasyFinanceApi.Context;
using EasyFinanceApi.Models.DTOs;
using EasyFinanceApi.Models.Entities;
using EasyFinanceApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyFinanceApi.Repository
{
    public class ExpenseRepository : BaseRepository, IExpenseRepository
    {
        private readonly EasyFinanceContext _context;

        public ExpenseRepository(EasyFinanceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Expense> GetExpenseByDescription(string description)
        {
            return await _context.Expenses.Where(x => x.Description == description).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpenses()
        {
            List<Expense> expenses = await _context.Expenses.ToListAsync();

            var expensesDTO = expenses.Select(e => new ExpenseDTO
            {
                Description = e.Description,
                Value = e.Value,
                Type = e.Type,
                Maturity = e.Maturity
            });

            return expensesDTO;
        }
    }
}
