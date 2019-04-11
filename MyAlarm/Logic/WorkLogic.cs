using Microsoft.EntityFrameworkCore;
using MyAlarm.EFStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public class WorkLogic : BaseLogic
    {
        public WorkLogic(string connectionString) : base(connectionString)
        {
        }

        public WorkLogic(dataContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Work> GetAllAsync()
        {
            IQueryable<Work> query = _DbContext.Work;
            query = query.AsNoTracking();
            return query;
        }
    }
}
