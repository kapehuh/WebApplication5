using Microsoft.EntityFrameworkCore;
using WebApplication5.Abstractions;

namespace WebApplication5.Repository
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _dataContext;
        public EFRepository(DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task AddAsync(T entity)
        {
            await _dataContext.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.id == id);
            if (entity != null)
            {
                _dataContext.Set<T>().Remove(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            var entity2 = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.id == entity.id);
            if (entity2 != null)
            {
                entity2 = entity;
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
