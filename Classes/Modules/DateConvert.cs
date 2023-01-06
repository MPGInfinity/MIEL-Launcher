using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MielLauncher.Classes.Modules
{
    internal static class DateConvert
    {
        public static DateTime StringToDateTime(string dateString)
        {
            return DateTime.ParseExact(dateString, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
        public static string FormatDateString(string dateString)
        {
            return StringToDateTime(dateString).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
