using BasicWebAPI.Core.Dtos;
using BasicWebAPI.Core.Entities;
using BasicWebAPI.Core.Interfaces;
using BasicWebAPI.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicWebAPI.Core.Services
{
    public class ExampleService : BaseService<ExampleEntity, ExampleDto>, IExampleService
    {
        public ExampleService(IRepository<ExampleEntity> repository) : base(repository)
        {
        }
    }
}
