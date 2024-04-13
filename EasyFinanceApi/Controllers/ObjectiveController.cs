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
                Objective objective = _mapper.Map<Objective>(objectiveDTO);

                _objectiveService.CreateObjective(objective, _objectiveRepository, _configHelper.GetDefaultUserId());

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
    }
}
