using Microsoft.EntityFrameworkCore;
using MyAlarm.EFStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ScrumFrameworkLogic : BaseLogic
    {
        public ScrumFrameworkLogic(string connectionString) : base(connectionString)
        {
        }

        public ScrumFrameworkLogic(dataContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<ScrumFramework> GetAllAsync()
        {
            IQueryable<ScrumFramework> query = _DbContext.ScrumFramework;
            query = query.AsNoTracking().OrderBy(h => h.Name);
            return query;
        }


        public Task<ScrumFramework> GetAsync(System.String id)
        {

            IQueryable<ScrumFramework> query = _DbContext.ScrumFramework;

            var item = query.FirstOrDefaultAsync(h => h.Id == id);

            return item;
        }
    }
}
