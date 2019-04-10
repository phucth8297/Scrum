using MyAlarm.EFStandard;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class BaseLogic
    {
        protected dataContext _DbContext { get; private set; }
        public BaseLogic(string connectionString)
        {
            _DbContext = new dataContext(connectionString);
        }

        public BaseLogic(dataContext dbContext)
        {
            _DbContext = dbContext;
        }
        public string CheckRole(Member member)
        {
            try
            {
                var isRole = _DbContext.Role.Where(h => h.Id == member.FkRole).FirstOrDefault();
                return isRole.Id;

            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
