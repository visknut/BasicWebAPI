using BasicWebAPI.Core.Shared;

namespace BasicWebAPI.Core.Interfaces
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        Status Status { get; set; }
    }
}
