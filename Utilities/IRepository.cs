using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public interface IRepository<t> where t:class
    {
        IEnumerable<t> Get();
        t Get(int id);
        void Create(t entity);
        void Update(t entity);
        void Delete(int id);
    }
}
