using EasyFinanceApi.Models.Entities;
using EasyFinanceApi.Repository.Interfaces;
using EasyFinanceApi.Services.Interfaces;

namespace EasyFinanceApi.Services
{
    public class ObjectiveService : IObjectiveService
    {
        public void CreateObjective(Objective objective, IObjectiveRepository objectiveRepository, int userId)
        {
            int monthsToObjective = ReturnMonthsToObjective(objective);

            objective.Description = objective.Description.ToLower();
            objective.MonthsToObjective = monthsToObjective;
            objective.CreationDate = DateTime.Now.ToUniversalTime();
            objective.CreationUserId = userId;

            objectiveRepository.Add(objective);
        }

        public static int ReturnMonthsToObjective(Objective objective)
        {
            string missingValue = (objective.Value - objective.CurrentValue).ToString().Replace(",", "");

            decimal monthsToObjective = int.Parse(missingValue) / objective.InvestmentMonth;

            return (int)Math.Round(monthsToObjective, MidpointRounding.AwayFromZero);
        }

        public async Task<Objective> ValidateObjectiveExistsAsync(string description, IObjectiveRepository objectiveRepository, bool createNewObjective)
        {
            if(string.IsNullOrEmpty(description))
                throw new Exception("Objetivo não encontrado na base de dados");

            description = description.ToLower();

            Objective objective = await objectiveRepository.GetObjectiveByDescription(description);

            if (createNewObjective)
            {
                if (objective != null)
                    throw new Exception("Objetivo já existe na base de dados");
                else
                    return objective;
            }
            else
            {
                if (objective == null)
                    throw new Exception("Objetivo não encontrado na base de dados");
                else
                    return objective;
            }
        }

        public void DeleteObjectiveByDescription(Objective objective, IObjectiveRepository objectiveRepository)
        {
            objectiveRepository.Delete(objective);
        }
    }
}