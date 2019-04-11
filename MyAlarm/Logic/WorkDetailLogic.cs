using Microsoft.EntityFrameworkCore;
using MyAlarm.EFStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class WorkDetailLogic : BaseLogic
    {
        public WorkDetailLogic(string connectionString) : base(connectionString)
        {
        }

        public WorkDetailLogic(dataContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<WorkDetail> GetAllAsync()
        {
            IQueryable<WorkDetail> query = _DbContext.WorkDetail;
            query = query.AsNoTracking();
            return query;
        }

        public Task<WorkDetail> GetAsync(System.String id)
        {

            IQueryable<WorkDetail> query = _DbContext.WorkDetail;

            var item = query.FirstOrDefaultAsync(h => h.FkWork == id);

            return item;
        }
    }
}
