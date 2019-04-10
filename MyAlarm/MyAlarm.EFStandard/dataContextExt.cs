using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyAlarm.EFStandard
{
    public partial class dataContext 
    {
        private DbContextOptionsBuilder<dataContext> _optionBuilder;
        public dataContext(string connectionString) : base(
            SqliteDbContextOptionsBuilderExtensions
                .UseSqlite(new DbContextOptionsBuilder(), connectionString)
                .Options
            )
        {
        }
    }
}
