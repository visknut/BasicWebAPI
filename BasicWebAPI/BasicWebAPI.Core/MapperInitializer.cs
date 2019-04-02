using System;
using System.Collections.Generic;
using System.Text;

namespace BasicWebAPI.Core
{
    public static class MapperInitializer
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {

            });
        }

    }
}
