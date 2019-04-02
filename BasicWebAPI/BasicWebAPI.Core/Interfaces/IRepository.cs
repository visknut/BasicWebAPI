using System.Collections.Generic;
using BasicWebAPI.Core.Specifications;

namespace BasicWebAPI.Core.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        TEntity Create(TEntity entity);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Delete(int id);

        TEntity Get(int id);
        IEnumerable<TEntity> GetBy(Specification<TEntity> genericSpecification);
    }
}
