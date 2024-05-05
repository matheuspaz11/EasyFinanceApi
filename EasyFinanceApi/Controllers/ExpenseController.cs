using AutoMapper;
using EasyFinanceApi.Helpers.Util;
using EasyFinanceApi.Models.DTOs;
using EasyFinanceApi.Models.Entities;
using EasyFinanceApi.Repository.Interfaces;
using EasyFinanceApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ExpenseService _expenseService;
        private readonly ConfigHelper _configHelper;

        public ExpenseController(IMapper mapper, IExpenseRepository expenseRepository, ConfigHelper configHelper, ExpenseService expenseService)
        {
            _mapper = mapper;
            _expenseRepository = expenseRepository;
            _configHelper = configHelper;
            _expenseService = expenseService;
        }

        [HttpPost]
        [Route("CreateExpense")]
        public async Task<IActionResult> CreateExpense(ExpenseDTO expenseDTO)
        {
            try
            {
                Expense newExpense = _mapper.Map<Expense>(expenseDTO);

                await _expenseService.ValidateExpenseExists(_expenseRepository, true, null, newExpense.Description);

                _expenseService.CreateNewExpense(newExpense, _expenseRepository, _configHelper.GetDefaultUserId());

                object result;
        
                if (await _expenseRepository.SaveChangesAsync())
                {
                    result = new { success = true, message = "Despesa salva com sucesso!", expense = newExpense};

                    return StatusCode(200, result);
                }
                else
                {
                    result = new { success = true, message = "Houve um erro no sistema, tente novamente mais tarde!" };

                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                await _expenseRepository.DisposeAsync();

                object result = new { Success = false, Message = ex.Message };

                return StatusCode(400, result);
            }
        }

        [HttpDelete]
        [Route("DeleteExpense/{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            try
            {
                Expense expense = await _expenseService.ValidateExpenseExists(_expenseRepository, false, id, null);

                _expenseService.DeleteExpenseByDescription(expense, _expenseRepository);

                object result;

                if (await _expenseRepository.SaveChangesAsync())
                {
                    result = new { Success = true, Message = "Despesa deletada com sucesso!" };

                    return StatusCode(200, result);
                }
                else
                {
                    result = new { Success = false, Message = "Não foi possível deletar a despesa" };

                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                await _expenseRepository.DisposeAsync();

                object result = new { Success = false, Message = ex.Message };

                return StatusCode(400, result);
            }
        }

        [HttpPatch]
        [Route("PayExpense/{id}")]
        public async Task<IActionResult> PayExpense(int id)
        {
            try
            {
                Expense expense = await _expenseService.ValidateExpenseExists(_expenseRepository, false, id, null);

                _expenseService.PayExpense(expense, _expenseRepository, _configHelper.GetDefaultUserId());

                object result;

                if (await _expenseRepository.SaveChangesAsync())
                {
                    result = new { Success = true, Message = "Despesa atualizada como paga!" };

                    return StatusCode(200, result);
                }
                else
                {
                    result = new { Success = false, Message = "Não foi possível atualizar despesa" };

                    return StatusCode (500, result);
                }
            }
            catch (Exception ex)
            {
                await _expenseRepository.DisposeAsync();

                object result = new { Success = false, Message = ex.Message };

                return StatusCode(400, result);
            }
        }

        [HttpPut]
        [Route("ChangeExpense")]
        public async Task<IActionResult> ChangeExpense(ChangeExpenseDTO changeExpenseDTO)
        {
            try
            {
                Expense expense = await _expenseService.ValidateExpenseExists(_expenseRepository, false, null, changeExpenseDTO.Description);

                if (changeExpenseDTO.Value == null && changeExpenseDTO.Type == null && changeExpenseDTO.Maturity == null)
                    return BadRequest("É necessário informar o campo Value, Type ou Maturity");

                Expense changeExpense = _mapper.Map<Expense>(changeExpenseDTO);

                _expenseService.ChangeExpense(expense, changeExpense, _expenseRepository, _configHelper.GetDefaultUserId());

                object result;

                if (await _expenseRepository.SaveChangesAsync())
                {
                    result = new { Success = true, Message = "Despesa atualizada com sucesso!" };

                    return StatusCode(200, result);
                }
                else
                {
                    result = new { Success = false, Message = "Houve um erro no sistema, tente novamente" };

                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                await _expenseRepository.DisposeAsync();

                object result = new { Success = false, Message = ex.Message };

                return StatusCode(400, result);
            }
        }

        [HttpGet]
        [Route("GetExpenses")]
        public async Task<IActionResult> GetExpenses()
        {
            object result;

            try
            {
                var expenses = await _expenseService.GetExpenses(_expenseRepository);

                result = new { Success = true, Result = expenses };

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                result = new { Success = false, Message = ex.Message };

                return StatusCode(400, result);
            }
        }

        [HttpGet]
        [Route("GetExpense/{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            object result;

            try
            {
                Expense expense = await _expenseService.GetExpenseById(id, _expenseRepository);

                result = new { Success = true, Result = expense };

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                result = new { Success = false, Message = ex.Message };

                return StatusCode(400, result);
            }
        }
    }
}