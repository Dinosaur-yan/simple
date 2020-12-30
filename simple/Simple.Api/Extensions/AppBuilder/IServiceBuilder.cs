using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple.Api.Extensions.AppBuilder
{
    public interface IServiceBuilder
    {
        void AddControllers();

        void AddSwaggerGen();
    }
}
