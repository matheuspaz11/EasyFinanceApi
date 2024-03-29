using AutoMapper;
using EasyFinanceApi.Helpers.Util;
using EasyFinanceApi.Models.DTOs;
using EasyFinanceApi.Models.Entities;
using EasyFinanceApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static EasyFinanceApi.Models.Enums.Enum;

namespace EasyFinanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ConfigHelper _configHelper;

        public ExpenseController(IMapper mapper, IExpenseRepository expenseRepository, ConfigHelper configHelper)
        {
            _mapper = mapper;
            _expenseRepository = expenseRepository;
            _configHelper = configHelper;
        }

        [HttpPost]
        [Route("CreateExpense")]
        public async Task<IActionResult> CreateExpense(ExpenseDTO expenseDTO)
        {
            try
            {
                Expense newExpense = _mapper.Map<Expense>(expenseDTO);

                newExpense.CreationDate = DateTime.Now.ToUniversalTime();
                newExpense.CreationUserId = _configHelper.GetDefaultUserId();

                _expenseRepository.Add(newExpense);

                return await _expenseRepository.SaveChangesAsync() ? Ok("Despesa salva com sucesso!") : BadRequest("Houve um erro no sistema, tente novamente mais tarde!");
            }
            catch (Exception ex)
            {
                _expenseRepository.DisposeAsync();

                return BadRequest(ex.Message);            }
        }

        [HttpGet]
        [Route("GetExpense/{description}")]
        public async Task<IActionResult> GetExpense(string description)
        {
            try
            {
                if (string.IsNullOrEmpty(description))
                    return BadRequest("Informe a despesa para busca");

                Expense expense = await _expenseRepository.GetExpenseByDescription(description);

                if (expense == null)
                    return BadRequest("Despesa não encontrada");

                var expenseResponse = _mapper.Map<GetExpenseDTO>(expense);

                return Ok(expenseResponse);
            }
            catch(Exception ex)
            {
                _expenseRepository.DisposeAsync();

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteExpense/{description}")]
        public async Task<IActionResult> DeleteExpense(string description)
        {
            try
            {
                if (string.IsNullOrEmpty(description))
                    return BadRequest("Informe a despesa para deletar");

                Expense expense = await _expenseRepository.GetExpenseByDescription(description);

                if (expense == null)
                    return BadRequest("Despesa não encontrada");
                else
                    _expenseRepository.Delete(expense);

                return await _expenseRepository.SaveChangesAsync() ? Ok("Despesa deletada com sucesso!") : BadRequest("Não foi possível deletar a despesa");
            }
            catch (Exception ex)
            {
                _expenseRepository.DisposeAsync();

                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("PayExpense/{description}")]
        public async Task<IActionResult> PayExpense(string description)
        {
            try
            {
                if (string.IsNullOrEmpty(description))
                    return BadRequest("Informe a despesa para pagar");

                var expense = await _expenseRepository.GetExpenseByDescription(description);

                if (expense == null)
                    return BadRequest("Despesa não encontrada");

                Expense expenseUpdate = _mapper.Map<Expense>(expense);

                if (expense.Status == ExpenseStatus.Liquidated)
                    return BadRequest("Despesa já paga");

                expenseUpdate.PaymentDate = DateTime.Now.ToUniversalTime();
                expenseUpdate.Status = ExpenseStatus.Liquidated;
                expenseUpdate.UpdateDate = DateTime.Now.ToUniversalTime();
                expenseUpdate.UpdateUserId = _configHelper.GetDefaultUserId();

                _expenseRepository.Update(expenseUpdate);

                return await _expenseRepository.SaveChangesAsync() ? Ok("Despesa foi atualizada como paga!") : BadRequest("Não foi possível atualizar despesa");
            }
            catch (Exception ex)
            {
                _expenseRepository.DisposeAsync();

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("ChangeExpense")]
        public async Task<IActionResult> ChangeExpense(ChangeExpenseDTO changeExpenseDTO)
        {
            try
            {
                if (changeExpenseDTO.Value == null && changeExpenseDTO.Type == null && changeExpenseDTO.Maturity == null)
                    return BadRequest("É necessário informar o campo Valor, Tipo ou Vencimento");

                Expense changeExpense = _mapper.Map<Expense>(changeExpenseDTO);

                if (string.IsNullOrEmpty(changeExpense.Description))
                    return BadRequest("O campo Description é obrigatório");

                Expense modifiedExpense = await _expenseRepository.GetExpenseByDescription(changeExpense.Description);

                if(modifiedExpense == null)
                    return NotFound("Despesa não encontrada na base de dados");

                if (modifiedExpense.Status.Equals(ExpenseStatus.Liquidated))
                    return BadRequest("Não é possível alterar uma despesa já paga");

                if (changeExpenseDTO.Value != null)
                    modifiedExpense.Value = changeExpense.Value;

                if(changeExpenseDTO.Maturity != null)
                    modifiedExpense.Maturity  = changeExpense.Maturity;

                if(changeExpenseDTO.Type != null)
                    modifiedExpense.Type = changeExpense.Type;

                modifiedExpense.UpdateDate = DateTime.Now.ToUniversalTime();
                modifiedExpense.UpdateUserId = _configHelper.GetDefaultUserId();

                _expenseRepository.Update(modifiedExpense);

                return await _expenseRepository.SaveChangesAsync() ? Ok("Despesa atualizada com sucesso!") : BadRequest("Houve um erro no sistema, tente novamente");

            }
            catch (Exception ex)
            {
                _expenseRepository.DisposeAsync();

                return BadRequest(ex.Message);
            }
        }
    }
}