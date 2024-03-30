using EasyFinanceApi.Models.Entities;
using EasyFinanceApi.Repository.Interfaces;

namespace EasyFinanceApi.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<Expense> ValidateExpenseExists(string description, IExpenseRepository expenseRepository);

        void CreateNewExpense(Expense expense, IExpenseRepository expenseRepository, int userId);

        void DeleteExpenseByDescription(Expense expense, IExpenseRepository expenseRepository);

        void PayExpense(Expense expense, IExpenseRepository expenseRepository, int userId);

        void ChangeExpense(Expense expense, Expense changedExpense, IExpenseRepository expenseRepository, int userId);
    }
}
