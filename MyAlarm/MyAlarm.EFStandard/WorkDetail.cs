using System;
using System.Collections.Generic;

namespace MyAlarm.EFStandard
{
    public partial class WorkDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FkWork { get; set; }
        public string FkUser { get; set; }
    }
}
