using AutoMapper;
using BasicWebAPI.Core.Interfaces;
using BasicWebAPI.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicWebAPI.Core.Services
{
    public abstract class BaseService<TEntity, Tdto> : ICommonOperations<Tdto>
        where TEntity : class, IEntity<int>
        where Tdto : class
    {
        protected IRepository<TEntity> _repository;

        public BaseService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual OperationResult<Tdto> Create(Tdto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);
            var newEntity = _repository.Create(entity);
            var result = Mapper.Map<Tdto>(newEntity);
            return OperationResult<Tdto>.CreateSuccessResult(result);
        }

        public virtual OperationResult<Tdto> Delete(int id)
        {
            var result = _repository.Get(id);
            if (result == null)
            {
                return OperationResult<Tdto>.CreateFailure(new InvalidOperationException("Error: The entity you tried to delete from the database was not found."));
            }
            result.Status = Status.Inactive;
            _repository.Update(result);
            var resultDto = Mapper.Map<Tdto>(result);
            return OperationResult<Tdto>.CreateSuccessResult(resultDto);
        }

        public virtual OperationResult<Tdto> Get(int id)
        {
            var result = _repository.Get(id);
            if (result == null)
            {
                return OperationResult<Tdto>.CreateFailure(new InvalidOperationException("Error: You tried to access an entity that wasn't present in the database."));
            }
            else if (result.Status == Status.Inactive)
            {
                return OperationResult<Tdto>.CreateFailure(new InvalidOperationException("Error: This entity was deleted from the inventory."));
            }
            else
            {
                var resultDto = Mapper.Map<Tdto>(result);
                return OperationResult<Tdto>.CreateSuccessResult(resultDto);
            }
        }

        public virtual OperationResult<IEnumerable<Tdto>> GetAll()
        {
            var entities = _repository.GetAll().ToList().Where(e => e.Status == Status.Active);
            var entitiesDtos = Mapper.Map<IEnumerable<Tdto>>(entities);
            return OperationResult<IEnumerable<Tdto>>.CreateSuccessResult(entitiesDtos);
        }

        public virtual OperationResult<Tdto> Update(Tdto dto, int id)
        {
            var result = Mapper.Map<Tdto>(_repository.Get(id));
            if (result == null)
            {
                return OperationResult<Tdto>.CreateFailure(new InvalidOperationException("Update failed: the entity you tried to update wasn't found in the database."));
            }
            var entity = Mapper.Map<TEntity>(dto);
            _repository.Update(entity);
            return OperationResult<Tdto>.CreateSuccessResult(result);
        }
    }
}
