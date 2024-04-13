using EasyFinanceApi.Context;
using EasyFinanceApi.Models.Entities;
using EasyFinanceApi.Repository.Interfaces;

namespace EasyFinanceApi.Repository
{
    public class ObjectiveRepository : BaseRepository, IObjectiveRepository
    {
        private readonly EasyFinanceContext _context;

        public ObjectiveRepository(EasyFinanceContext context) : base(context)
        {
            _context = context;
        }

        public Task<Objective> GetObjectiveByDescription(string description)
        {
            throw new NotImplementedException();
        }
    }
}
