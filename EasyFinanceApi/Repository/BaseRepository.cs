using EasyFinanceApi.Context;
using EasyFinanceApi.Repository.Interfaces;

namespace EasyFinanceApi.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly EasyFinanceContext _context;

        public BaseRepository(EasyFinanceContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
