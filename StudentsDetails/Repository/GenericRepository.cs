using Microsoft.EntityFrameworkCore;
using StudentsDetails.Context;
using StudentsDetails.Repository.IRepository;
using System.Linq.Expressions;

namespace StudentsDetails.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbcontext;

        public GenericRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task CreateAsync(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbcontext.Remove(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var allItem = await _dbcontext.Set<T>().AsNoTracking().ToListAsync();

            return allItem;
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> condition)
        {
            var existItem = await _dbcontext.Set<T>().AsNoTracking().FirstOrDefaultAsync(condition);

            return existItem;
        }
    }
}
