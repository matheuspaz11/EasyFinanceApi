using EasyFinanceApi.Models.Entities;

namespace EasyFinanceApi.Repository.Interfaces
{
    public interface IObjectiveRepository : IBaseRepository
    {
        Task<Objective> GetObjectiveByDescription(string description); 
    }
}
