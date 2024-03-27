using AutoMapper;
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

        public ExpenseController(IMapper mapper, IExpenseRepository expenseRepository)
        {
            _mapper = mapper;
            _expenseRepository = expenseRepository;
        }

        [HttpPost]
        [Route("CreateExpense")]
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

        [HttpGet]
        [Route("GetExpense/{description}")]
        public async Task<IActionResult> GetExpense(string description)
        {
            try
            {
                if (string.IsNullOrEmpty(description))
                    return BadRequest("Informe a despesa para busca");

                var expense = await _expenseRepository.GetExpenseByDescription(description);

                var expenseResponse = _mapper.Map<GetExpenseDTO>(expense);

                return expenseResponse != null ? Ok(expenseResponse) : BadRequest("Despesa não foi encontrada");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteExpense/{description}")]
        public async Task<IActionResult> DeleteExpense(string description)
        {
            try
            {
                if (string.IsNullOrEmpty(description))
                    return BadRequest("Informe a despesa para busca");

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

                throw;
            }
        }

        [HttpPatch]
        [Route("PayExpense/{description}")]
        public async Task<IActionResult> PayExpense(string description)
        {
            try
            {
                if (string.IsNullOrEmpty(description))
                    return BadRequest("Informe a despesa para busca");

                var expense = await _expenseRepository.GetExpenseByDescription(description);

                if (expense == null)
                    return BadRequest("Despesa não encontrada");

                Expense expenseUpdate = _mapper.Map<Expense>(expense);

                if (expense.Status == ExpenseStatus.Liquidated)
                    return BadRequest("Despesa já paga");

                expenseUpdate.PaymentDate = DateTime.Now.ToUniversalTime();
                expenseUpdate.Status = ExpenseStatus.Liquidated;

                _expenseRepository.Update(expenseUpdate);

                return await _expenseRepository.SaveChangesAsync() ? Ok("Despesa foi atualizada como paga!") : BadRequest("Não foi possível atualizar despesa");
            }
            catch (Exception ex)
            {
                _expenseRepository.DisposeAsync();

                throw;
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

                var changeExpense = _mapper.Map<Expense>(changeExpenseDTO);

                if (string.IsNullOrEmpty(changeExpense.Description))
                    return BadRequest("O campo Description é obrigatório");

                var modifiedExpense = await _expenseRepository.GetExpenseByDescription(changeExpense.Description);

                if(modifiedExpense == null)
                    return NotFound("Despesa não encontrada na base de dados");

                if (changeExpenseDTO.Value != null)
                    modifiedExpense.Value = changeExpense.Value;

                if(changeExpenseDTO.Maturity != null)
                    modifiedExpense.Maturity  = changeExpense.Maturity;

                if(changeExpenseDTO.Type != null)
                    modifiedExpense.Type = changeExpense.Type;

                _expenseRepository.Update(modifiedExpense);

                return await _expenseRepository.SaveChangesAsync() ? Ok("Despesa atualizada com sucesso!") : BadRequest("Houve um erro no sistema, tente novamente");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}