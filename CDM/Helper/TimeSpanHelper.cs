using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDM.Helper
{
    public class TimeSpanHelper
    {
        public static string FormatTimeSpan(TimeSpan? timeSpan)
        {
            // Construct the formatted string
            if (timeSpan.HasValue)
            {
                // Construct the formatted string
                string formattedTimeSpan = string.Format("{0:%h}:{0:%m}", timeSpan.Value);
                return formattedTimeSpan;
            }
            else
            {
                return "N/A"; // or any other default value you prefer
            }

           
        }
        public static string FormatDateTime(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString("MM/dd/yyyy"); // Customize the format as needed
            }
            else
            {
                return "N/A"; // or any other default value you prefer
            }
        }
    }
}