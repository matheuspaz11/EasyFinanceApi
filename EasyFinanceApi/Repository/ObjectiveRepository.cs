using EasyFinanceApi.Context;
using EasyFinanceApi.Models.Entities;
using EasyFinanceApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyFinanceApi.Repository
{
    public class ObjectiveRepository : BaseRepository, IObjectiveRepository
    {
        private readonly EasyFinanceContext _context;

        public ObjectiveRepository(EasyFinanceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Objective> GetObjectiveByDescription(string description)
        {
            return await _context.Objectives.Where(x => x.Description == description).FirstOrDefaultAsync();
        }
    }
}