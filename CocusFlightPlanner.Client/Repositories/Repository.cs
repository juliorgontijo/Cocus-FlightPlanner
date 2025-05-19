using CocusFlightPlanner.Application.Infra;
using CocusFlightPlanner.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CocusFlightPlanner.Application.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Upsert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                var entry = _context.Entry(entity);

                if (entry.State == EntityState.Detached)
                {
                    var pkey = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey();
                    var keyProps = pkey.Properties;

                    var keyValues = keyProps
                        .Select(p => typeof(T).GetProperty(p.Name).GetValue(entity))
                        .ToArray();

                    var existingEntity = await _context.Set<T>().FindAsync(keyValues);

                    if (existingEntity == null)
                    {
                        await _context.Set<T>().AddAsync(entity); // insert
                    }
                    else
                    {
                        _context.Entry(existingEntity).CurrentValues.SetValues(entity); // update
                    }
                }
                else if (entry.State == EntityState.Added)
                {
                    await _context.Set<T>().AddAsync(entity); 
                }
                else
                {
                    entry.State = EntityState.Modified; 
                }

                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public virtual async Task Delete(T entity)
        {
            entity.Deleted = true;
            await Upsert(entity);
            return;
        }

        public virtual void DeleteAll(IEnumerable<T> entities)
        {
            _context.RemoveRange(entities);
            return;
        }

        public virtual bool Exist(Func<T, bool> where, params Expression<Func<T, object>>[] includeProperties)
        {
            if (includeProperties.Any())
                return Include(_context.Set<T>(), includeProperties).Any(where);
            return _context.Set<T>().Any(where);
        }

        public virtual async Task<T> GetById(bool readOnly, Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            if (includeProperties.Any())
                return await Queryable(readOnly, includeProperties).FirstOrDefaultAsync(x => x.Id == id);
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> By(bool readOnly, Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                return await Queryable(readOnly, includeProperties).FirstOrDefaultAsync(where);

            }
            catch (Exception e)
            {
                var x = e;
                throw;
            }
        }

        public virtual async Task Create(T entity)
        {
            if (_context.Entry(entity).State != EntityState.Added)
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<IEnumerable<T>> GetAll(bool readOnly, params Expression<Func<T, object>>[] includeProperties)
        {
            return await Queryable(readOnly, includeProperties).ToListAsync();
        }

        private IQueryable<T> Include(IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties)
        {
            foreach (var property in includeProperties)
                query = query.Include(property);

            return query;
        }

        private IQueryable<T> Queryable(bool readOnly,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includeProperties.Any())
                query = Include(_context.Set<T>(), includeProperties);

            if (readOnly)
                query.AsNoTracking();

            return query;
        }

    }
}