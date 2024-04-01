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

                await _expenseService.ValidateExpenseExists(newExpense.Description, _expenseRepository, true);

                _expenseService.CreateNewExpense(newExpense, _expenseRepository, _configHelper.GetDefaultUserId());

                object result;
        
                if (await _expenseRepository.SaveChangesAsync())
                {
                    result = new { success = true, message = "Despesa salva com sucesso!" };

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

        [HttpGet]
        [Route("GetExpense/{description}")]
        public async Task<IActionResult> GetExpense(string description)
        {
            try
            {
                Expense expense = await _expenseService.ValidateExpenseExists(description, _expenseRepository, false);

                GetExpenseDTO expenseResponse = _mapper.Map<GetExpenseDTO>(expense);

                object result = new { Success = true,  Expense = expenseResponse };
                
                return StatusCode(200, result);
            }
            catch(Exception ex)
            {
                object result = new { Success = false, Message =  ex.Message};

                return StatusCode(400, result);
            }
        }

        [HttpDelete]
        [Route("DeleteExpense/{description}")]
        public async Task<IActionResult> DeleteExpense(string description)
        {
            try
            {
                Expense expense = await _expenseService.ValidateExpenseExists(description, _expenseRepository, false);

                _expenseService.DeleteExpenseByDescription(expense, _expenseRepository);

                return await _expenseRepository.SaveChangesAsync() ? Ok("Despesa deletada com sucesso!") : BadRequest("Não foi possível deletar a despesa");
            }
            catch (Exception ex)
            {
                await _expenseRepository.DisposeAsync();

                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("PayExpense/{description}")]
        public async Task<IActionResult> PayExpense(string description)
        {
            try
            {
                Expense expense = await _expenseService.ValidateExpenseExists(description, _expenseRepository, false);

                _expenseService.PayExpense(expense, _expenseRepository, _configHelper.GetDefaultUserId());

                return await _expenseRepository.SaveChangesAsync() ? Ok("Despesa foi atualizada como paga!") : BadRequest("Não foi possível atualizar despesa");
            }
            catch (Exception ex)
            {
                await _expenseRepository.DisposeAsync();

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("ChangeExpense")]
        public async Task<IActionResult> ChangeExpense(ChangeExpenseDTO changeExpenseDTO)
        {
            try
            {
                Expense expense = await _expenseService.ValidateExpenseExists(changeExpenseDTO.Description, _expenseRepository, false);

                if (changeExpenseDTO.Value == null && changeExpenseDTO.Type == null && changeExpenseDTO.Maturity == null)
                    return BadRequest("É necessário informar o campo Value, Type ou Maturity");

                Expense changeExpense = _mapper.Map<Expense>(changeExpenseDTO);

                _expenseService.ChangeExpense(expense, changeExpense, _expenseRepository, _configHelper.GetDefaultUserId());

                return await _expenseRepository.SaveChangesAsync() ? Ok("Despesa atualizada com sucesso!") : BadRequest("Houve um erro no sistema, tente novamente");
            }
            catch (Exception ex)
            {
                await _expenseRepository.DisposeAsync();

                return BadRequest(ex.Message);
            }
        }
    }
}