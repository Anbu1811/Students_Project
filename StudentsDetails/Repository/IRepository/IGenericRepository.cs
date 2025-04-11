using System.Linq.Expressions;

namespace StudentsDetails.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync(T entity);

        Task  DeleteAsync(T entity);

        Task<T> GetByIdAsync(Expression<Func<T, bool>> condition);

        Task<IEnumerable<T>> GetAllAsync();
    }
}
