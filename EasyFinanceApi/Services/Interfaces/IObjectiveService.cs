using EasyFinanceApi.Models.Entities;
using EasyFinanceApi.Repository.Interfaces;

namespace EasyFinanceApi.Services.Interfaces
{
    public interface IObjectiveService
    {
        void CreateObjective(Objective objective, IObjectiveRepository objectiveRepository, int userId);
    }
}
