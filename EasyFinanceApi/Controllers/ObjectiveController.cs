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
    public class ObjectiveController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IObjectiveRepository _objectiveRepository;
        private readonly ObjectiveService _objectiveService;
        private readonly ConfigHelper _configHelper;

        public ObjectiveController(IMapper mapper, IObjectiveRepository objectiveRepository, ObjectiveService objectiveService, ConfigHelper configHelper)
        {
            _mapper = mapper;
            _objectiveRepository = objectiveRepository;
            _objectiveService = objectiveService;
            _configHelper = configHelper;
        }

        [HttpPost]
        [Route("CreateObjective")]
        public async Task<IActionResult> CreateObjective(ObjectiveDTO objectiveDTO)
        {
            try
            {
                Objective newObjective = _mapper.Map<Objective>(objectiveDTO);

                await _objectiveService.ValidateObjectiveExistsAsync(newObjective.Description, _objectiveRepository, true);

                _objectiveService.CreateObjective(newObjective, _objectiveRepository, _configHelper.GetDefaultUserId());

                object result;

                if (await _objectiveRepository.SaveChangesAsync())
                {
                    result = new { Success = true, Message = "Objetivo criado com sucesso!" };

                    return StatusCode(200, result);
                }
                else
                {
                    result = new { Success = false, Message = "Houve um erro no sistema, tente novamente mais tarde" };

                    return StatusCode(500, result);
                }
            }
            catch(Exception ex) 
            {
                object result = new { Success = false, Message = ex.Message };

                return StatusCode(500, result);
            }
        }

        [HttpGet]
        [Route("GetObjective/{description}")]
        public async Task<IActionResult> GetObjective(string description)
        {
            try
            {
                Objective objective = await _objectiveService.ValidateObjectiveExistsAsync(description, _objectiveRepository, false);

                GetObjectiveDTO getObjectiveDTO = _mapper.Map<GetObjectiveDTO>(objective);

                object result = new { Success = true, Objective = getObjectiveDTO };

                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                object result = new { Success = false, Message = ex.Message };

                return StatusCode(400, result);
            }
        }

        [HttpDelete]
        [Route("DeleteObjective/{description}")]
        public async Task<IActionResult> DeleteObjective(string description)
        {
            try
            {
                Objective objective = await _objectiveService.ValidateObjectiveExistsAsync(description, _objectiveRepository, false);

                _objectiveService.DeleteObjectiveByDescription(objective, _objectiveRepository);

                object result;

                if (await _objectiveRepository.SaveChangesAsync())
                {
                    result = new { Success = true, Message = "Objetivo deletado com sucesso!" };

                    return StatusCode(200, result);
                }
                else
                {
                    result = new { Success = false, Message = "Não foi possível deletar o objetivo" };

                    return StatusCode(500, result);
                }
            }
            catch (Exception ex)
            {
                await _objectiveRepository.DisposeAsync();

                object result = new { Success = false, Message = ex.Message};

                return StatusCode(400, result);
            }
        }
    }
}