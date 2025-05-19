using CocusFlightPlanner.Common.Commands;
using CocusFlightPlanner.Common.Models;
using System.Linq.Expressions;

namespace CocusFlightPlanner.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(bool readOnly, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetById(bool readOnly, Guid id, params Expression<Func<T, object>>[] includeProperties);
        Task<T> Upsert(T entity);
        Task Delete(T entity);
    }    
}