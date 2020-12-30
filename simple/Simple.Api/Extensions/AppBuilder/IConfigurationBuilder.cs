using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple.Api.Extensions.AppBuilder
{
    public interface IConfigurationBuilder
    {
        void UseDefaultConfiguration();

        void UseEndpointsAndAuth();

        void UseSwagger();
    }
}
