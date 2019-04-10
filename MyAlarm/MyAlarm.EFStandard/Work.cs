using System;
using System.Collections.Generic;

namespace MyAlarm.EFStandard
{
    public partial class Work
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Des { get; set; }
        public string FkStatus { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string HourWork { get; set; }
    }
}
