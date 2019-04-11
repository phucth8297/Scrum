using Microsoft.EntityFrameworkCore;
using MyAlarm.EFStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class StatusLogic : BaseLogic
    {
        public StatusLogic(string connectionString) : base(connectionString)
        {
        }

        public StatusLogic(dataContext dbContext) : base(dbContext)
        {
        }

        public Task<Status> GetAsync(System.String id)
        {

            IQueryable<Status> query = _DbContext.Status;

            var item = query.FirstOrDefaultAsync(h => h.Id == id);

            return item;
        }
    }
}
