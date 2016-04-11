using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Utils
    {
        public static string getSQLDateFromDate(DateTime date)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(date.Year).Append("-").Append(date.Month).Append("-").Append(date.Day)
                .Append(" ").Append(date.Hour).Append(":").Append(date.Minute).Append(":").Append(date.Second);
            return stringBuilder.ToString();
        }
    }
}
