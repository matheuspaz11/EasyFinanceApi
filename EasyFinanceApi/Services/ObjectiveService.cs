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
    }
}
