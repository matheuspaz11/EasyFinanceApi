using EasyFinanceApi.Models.DTOs;
using EasyFinanceApi.Models.Entities;
using EasyFinanceApi.Repository.Interfaces;

namespace EasyFinanceApi.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<Expense> ValidateExpenseExists(IExpenseRepository expenseRepository, bool createNewExpense, int? id, string? description);

        void CreateNewExpense(Expense expense, IExpenseRepository expenseRepository, int userId);

        void DeleteExpenseByDescription(Expense expense, IExpenseRepository expenseRepository);

        void PayExpense(Expense expense, IExpenseRepository expenseRepository, int userId);

        void ChangeExpense(Expense expense, Expense changedExpense, IExpenseRepository expenseRepository, int userId);

        Task<IEnumerable<GetExpenseDTO>> GetExpenses(IExpenseRepository expenseRepository);

        Task<Expense> GetExpenseById(int id, IExpenseRepository expenseRepository);
    }
}
