using MyAlarm.EFStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class RoleLogic : BaseLogic
    {
        public RoleLogic(string connectionString) : base(connectionString)
        {

        }
        public RoleLogic(dataContext dbContext) : base(dbContext)
        {

        }
        

    }
}
