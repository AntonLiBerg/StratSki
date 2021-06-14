using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkiWebApi.Responses
{
    public abstract class IResponse
    {
        public string Name { get; set; }
        public IResponse()
        {
            Name = this.GetType().Name;
        }
    }
}
