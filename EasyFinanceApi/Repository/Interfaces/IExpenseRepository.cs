using EasyFinanceApi.Models.DTOs;
using EasyFinanceApi.Models.Entities;

namespace EasyFinanceApi.Repository.Interfaces
{
    public interface IExpenseRepository : IBaseRepository
    {
        Task<Expense> GetExpenseByDescription (string description);

        Task<IEnumerable<GetExpenseDTO>> GetExpenses();

        Task<Expense> GetExpenseById(int? id);
    }
}
