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

        public async Task<IEnumerable<GetExpenseDTO>> GetExpenses()
        {
            List<Expense> expenses = await _context.Expenses.ToListAsync();

            var expensesDTO = expenses.Select(e => new GetExpenseDTO
            {
                Id = e.Id,
                Description = e.Description,
                Value = e.Value,
                Type = e.Type.ToString(),
                Maturity = e.Maturity,
                Status = e.Status.ToString(),
                PaymentDate = e.PaymentDate,
                CreationDate = e.CreationDate,
                UpdateDate = e.UpdateDate
            });

            return expensesDTO;
        }

        public async Task<Expense> GetExpenseById(int? id)
        {
            return await _context.Expenses.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
