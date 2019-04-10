using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MyAlarm.Converter
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is int item)
            {
                return item == 0 ? "Nam" : "Nữ";
            }

            return "";
        }        

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

            // Co the implement phuong thuc nay neu convert la 2 chieu
            if (value is string strValue)
            {
                return strValue == "Co" ? true : false;
            }

            return false;

        }
       
    }
}
