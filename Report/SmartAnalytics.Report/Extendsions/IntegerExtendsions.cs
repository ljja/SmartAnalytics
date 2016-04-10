using System;

namespace SmartAnalytics.Report.Extendsions
{
    public static class IntegerExtendsions
    {
        public static string ToTimeSpanString(this int origin)
        {
            return string.Format("{0}:00~{0}:59", origin.ToString("D2"));
        }

        public static string ToTimeSpan(this int origin)
        {
            return TimeSpan.FromSeconds(origin).ToString();
        }
    }
}