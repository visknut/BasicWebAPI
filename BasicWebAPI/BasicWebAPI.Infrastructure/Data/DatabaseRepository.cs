using BasicWebAPI.Core.Interfaces;
using BasicWebAPI.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BasicWebAPI.Infrastructure.Data
{
    public class DatabaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        internal DbContext _context;
        internal DbSet<TEntity> _dbSet;

        public DatabaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            _context.Add<TEntity>(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var entity = _context.Find<TEntity>(id);
            _context.Remove<TEntity>(entity);
            _context.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            _context.Update<TEntity>(entity);
            _context.SaveChanges();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public virtual TEntity Get(int id)
        {
            return _dbSet.AsNoTracking().SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<TEntity> GetBy(Specification<TEntity> genericSpecification)
        {
            IQueryable<TEntity> queryable = _dbSet;

            var queryableResultWithIncludes = genericSpecification.Includes
                .Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));

            return queryableResultWithIncludes
                            .AsNoTracking()
                            .Where(genericSpecification.Criteria)
                            .AsEnumerable();
        }
    }
}
