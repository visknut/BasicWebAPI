using BasicWebAPI.Core.Interfaces;

namespace BasicWebAPI.Core.Dtos
{
    public class ExampleDto : IDto<int>
    {
        public int Id { get; set; }
    }
}
