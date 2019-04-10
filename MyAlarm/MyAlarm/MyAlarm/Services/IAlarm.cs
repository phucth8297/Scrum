using System;
using System.Collections.Generic;
using System.Text;

namespace MyAlarm.Services
{
    public interface IAlarm
    {
        void SetAlarm(DateTime time);
    }
}
