using BasicWebAPI.Core.Interfaces;
using BasicWebAPI.Core.Shared;

namespace BasicWebAPI.Core.Entities
{
    public class ExampleEntity : IEntity<int>
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}
