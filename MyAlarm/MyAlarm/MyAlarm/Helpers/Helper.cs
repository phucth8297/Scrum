using System;
using System.Collections.Generic;
using System.Text;

namespace MyAlarm.Helpers
{
    class Helper
    {
        public static string GetConnectionString()
        {
            return (App.Current as App).DbConnectionString;
        }
    }
}
