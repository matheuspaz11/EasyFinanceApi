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
        private readonly IBaseRepository _repository;
        private readonly IMapper _mapper;

        public ExpenseController(IBaseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(ExpenseDTO expenseDTO)
        {
            Expense newExpense = _mapper.Map<Expense>(expenseDTO);

            _repository.Add(newExpense);

            return await _repository.SaveChangesAsync() ? Ok("Despesa salva com sucesso!") : BadRequest("Houve um erro no sistema, tente novamente mais tarde!");
        }
    }
}
