using EasyFinanceApi.Models.DTOs;
using EasyFinanceApi.Models.Entities;
using EasyFinanceApi.Repository.Interfaces;
using EasyFinanceApi.Services.Interfaces;
using static EasyFinanceApi.Models.Enums.Enum;

namespace EasyFinanceApi.Services
{
    public class ExpenseService : IExpenseService
    {
        public async Task<Expense> ValidateExpenseExists(IExpenseRepository expenseRepository, bool createNewExpense, int? id, string? description)
        {
            if (createNewExpense)
            {
                description = description.ToLower();

                Expense expense = await expenseRepository.GetExpenseByDescription(description);

                if (expense != null)
                    throw new Exception("Despesa já cadastrada na base de dados");
                else
                    return expense;
            }
            else
            {
                Expense expense = await expenseRepository.GetExpenseById(id);

                if (expense == null)
                    throw new Exception("Despesa não encontrada na base de dados");
                else
                    return expense;
            }
        }

        public void CreateNewExpense(Expense expense, IExpenseRepository expenseRepository, int userId)
        {
            expense.Description = expense.Description.ToLower();
            expense.CreationDate = DateTime.Now.ToUniversalTime();
            expense.CreationUserId = userId;

            expenseRepository.Add(expense);
        }

        public void DeleteExpenseByDescription(Expense expense, IExpenseRepository expenseRepository)
        {
            expenseRepository.Delete(expense);
        }

        public void PayExpense(Expense expense, IExpenseRepository expenseRepository, int userId)
        {
            if (expense.Status == ExpenseStatus.Liquidated)
                throw new Exception("Despesa já paga");

            expense.PaymentDate = DateTime.Now.ToUniversalTime();
            expense.Status = ExpenseStatus.Liquidated;
            expense.UpdateDate = DateTime.Now.ToUniversalTime();
            expense.UpdateUserId = userId;

            expenseRepository.Update(expense);
        }

        public void ChangeExpense(Expense expense, Expense changedExpense, IExpenseRepository expenseRepository,  int userId)
        {
            bool updated = false;

            if (expense.Status.Equals(ExpenseStatus.Liquidated))
                throw new Exception("Não é possível alterar uma despesa já paga");

            if (!Enum.IsDefined(typeof(ExpenseType), changedExpense.Type))
                throw new Exception("Type inválido, forneça um type válido");

            if (changedExpense.Value > 0 && changedExpense.Value != expense.Value)
            {
                expense.Value = changedExpense.Value;
                updated = true;
            }

            if (changedExpense.Maturity > 0 && changedExpense.Maturity != expense.Maturity)
            {
                expense.Maturity = changedExpense.Maturity;
                updated = true;
            }

            if (!changedExpense.Type.Equals(expense.Type))
            {
                expense.Type = changedExpense.Type;
                updated = true;
            }

            if (updated)
            {
                expense.UpdateDate = DateTime.Now.ToUniversalTime();
                expense.UpdateUserId = userId;
            }else
                throw new Exception("Despesa já está atualizada de acordo com o que foi solicitado");

            expenseRepository.Update(expense);
        }

        public async Task<IEnumerable<GetExpenseDTO>> GetExpenses(IExpenseRepository expenseRepository)
        {
            var expenses = await expenseRepository.GetExpenses();

            if (!expenses.Any())
                throw new Exception("Não existe despesas cadastradas na base de dados");
            else
                return expenses;
        }

        public async Task<Expense> GetExpenseById(int id, IExpenseRepository expenseRepository)
        {
            Expense expense = await expenseRepository.GetExpenseById(id);

            if (expense == null)
                throw new Exception("Não existe despesas cadastradas na base de dados");
            else
                return expense;
        }
    }
}
