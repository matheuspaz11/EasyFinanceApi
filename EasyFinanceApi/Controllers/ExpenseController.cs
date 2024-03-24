using AutoMapper;
using EasyFinanceApi.Models.DTOs;
using EasyFinanceApi.Models.Entities;
using EasyFinanceApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseController(IMapper mapper, IExpenseRepository expenseRepository)
        {
            _mapper = mapper;
            _expenseRepository = expenseRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(ExpenseDTO expenseDTO)
        {
            try
            {
                Expense newExpense = _mapper.Map<Expense>(expenseDTO);

                _expenseRepository.Add(newExpense);

                return await _expenseRepository.SaveChangesAsync() ? Ok("Despesa salva com sucesso!") : BadRequest("Houve um erro no sistema, tente novamente mais tarde!");
            }
            catch (Exception ex)
            {
                _expenseRepository.DisposeAsync();

                throw;
            }
        }

        [HttpGet("{description}")]
        public async Task<IActionResult> GetExpenseByDescription(string description)
        {
            try
            {
                if (string.IsNullOrEmpty(description))
                    BadRequest("Informe a despesa para busca");

                var expense = await _expenseRepository.GetExpenseByDescription(description);

                var expenseResponse = _mapper.Map<ExpenseDTO>(expense);

                return expenseResponse != null ? Ok(expenseResponse) : BadRequest("Despesa não encontrada");
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
