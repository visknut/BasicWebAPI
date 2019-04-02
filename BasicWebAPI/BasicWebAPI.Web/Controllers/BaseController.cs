using BasicWebAPI.Core;
using BasicWebAPI.Core.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BasicWebAPI.Web.Controllers
{
    public abstract class BaseController<TDto> : ControllerBase
        where TDto : class, IDto<int>
    {
        protected ICommonOperations<TDto> _service;
        private string _controllerName;

        public BaseController(ICommonOperations<TDto> service, string controllerName)
        {
            _service = service;
            _controllerName = controllerName;
        }

        public virtual IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        public virtual IActionResult Get(int id)
        {
            var result = _service.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        public virtual IActionResult Create([FromBody] TDto newDto)
        {
            if (newDto == null)
            {
                return BadRequest(OperationResult<TDto>.CreateFailure(new Exception("Error: no data entered.")));
            }
            else if (!ModelState.IsValid)
            {
                return BadRequest(OperationResult<TDto>.CreateFailure(new Exception($"Error: {ModelState.Values.First().Errors[0].ErrorMessage}")));
            }

            var result = _service.Create(newDto);
            return CreatedAtRoute("Get_" + _controllerName, new { id = result.ResultData.Id }, result.ResultData);
        }

        public virtual IActionResult Update([FromBody] TDto UpdatedDto, int id)
        {
            if (UpdatedDto == null)
            {
                return BadRequest(OperationResult<TDto>.CreateFailure(new Exception("Error: no data entered.")));
            }
            else if (!ModelState.IsValid)
            {
                var some = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                return BadRequest(OperationResult<TDto>.CreateFailure(new Exception($"Error: {some.First()}")));
            }

            var result = _service.Update(UpdatedDto, id);

            if (result.Success)
            {
                return NoContent();
            }
            else
            {
                return NotFound(result);
            }
        }

        public virtual IActionResult PartiallyUpdate([FromBody] JsonPatchDocument<TDto> dtoPatch, int id)
        {
            if (dtoPatch == null)
            {
                return BadRequest(OperationResult<TDto>.CreateFailure(new Exception("Error: no data entered.")));
            }

            var result = _service.Get(id);

            if (!result.Success)
            {
                return NotFound(result);
            }

            dtoPatch.ApplyTo(result.ResultData, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(OperationResult<TDto>.CreateFailure(new Exception($"Error: {ModelState.Values.First().Errors[0].ErrorMessage}")));
            }

            _service.Update(result.ResultData, id);

            return NoContent();
        }

        public virtual IActionResult Delete(int id)
        {
            var result = _service.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
