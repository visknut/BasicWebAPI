using BasicWebAPI.Core.Dtos;
using BasicWebAPI.Core.Interfaces.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BasicWebAPI.Web.Controllers
{
    [Route("api/example")]
    [ApiController]
    public class ExampleController : BaseController<ExampleDto>
    {
        private const string _name = nameof(ExampleController);

        public ExampleController(IExampleService service) : base(service, _name)
        {
        }

        [HttpGet]
        public override IActionResult GetAll()
        {
            return base.GetAll();
        }

        [HttpGet("{id}", Name = "Get_" + _name)]
        public override IActionResult Get(int id)
        {
            return base.Get(id);
        }

        [HttpPost]
        public override IActionResult Create([FromBody] ExampleDto newDto)
        {
            return base.Create(newDto);
        }

        [HttpPut("{id}")]
        public override IActionResult Update([FromBody] ExampleDto updatedDto, int id)
        {
            return base.Update(updatedDto, id);
        }

        [HttpPatch("{id}")]
        public override IActionResult PartiallyUpdate([FromBody] JsonPatchDocument<ExampleDto> dtoPatch, int id)
        {
            return base.PartiallyUpdate(dtoPatch, id);
        }

        [HttpDelete("{id}")]
        public override IActionResult Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
