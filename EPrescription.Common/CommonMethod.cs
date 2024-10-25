using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPrescription.Common
{
  public static  class CommonMethod
    {
        public static string DateFormat(this DateTime? datetime)
        {
            return datetime.HasValue? string.Format("{0:dd-MMM-yyyy}", datetime.Value) :"";
        }
        public static string DateFormat(this DateTime datetime)
        {
            return string.Format("{0:dd-MMM-yyyy}", datetime);
        }
        public static string StatusString(this byte? value)
        {
            return value.HasValue? Enum.GetName(typeof(EnumActiveDeative), value).Replace("_"," ") :"";
        }
        public static bool IsActive(this byte? value)
        {
            return value.HasValue ? value== (byte)EnumActiveDeative.Active: false;
        }
        public static string JoinString(this IEnumerable<string> value,string separtor)
        {
            return String.Join(separtor, value);
        }
    }
}
