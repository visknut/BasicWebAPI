using System.Collections.Generic;

namespace BasicWebAPI.Core.Interfaces
{
    public interface ICommonOperations<TDto>
        where TDto : class
    {
        OperationResult<IEnumerable<TDto>> GetAll();
        OperationResult<TDto> Get(int id);
        OperationResult<TDto> Create(TDto dto);
        OperationResult<TDto> Update(TDto dto, int id);
        OperationResult<TDto> Delete(int id);
    }
}
