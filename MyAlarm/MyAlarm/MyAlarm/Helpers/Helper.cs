using System;
using System.Collections.Generic;
using System.Text;

namespace MyAlarm.Helpers
{
    class Helper
    {

        public static string KEY_WORK_OBJECT = "KEY_WORK_OBJECT";

        public static string KEY_SCRUMFRAMEWORK_OBJECT = "KEY_SCRUMFRAMEWORK_OBJECT";

        public static string GetConnectionString()
        {
            return (App.Current as App).DbConnectionString;
        }
    }
}
