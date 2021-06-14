using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Utilities
{
    public abstract class IDbContext:DbContext
    {
        public IDbContext():base("StratSkiDB")
        {

        }

        //This is only here so that I can ignore setting up a database
        public override int SaveChanges()
        {
            return 1;
        }
    }
}
