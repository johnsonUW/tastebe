using System;

namespace Framework.Utils
{
    public static class DateTimeExtensions
    {
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long SecondsSinceEpoch(this DateTime dt)
        {
            return (long)(dt - Epoch).TotalSeconds;
        }

        public static DateTime DateTimeFromEpoch(this long epoch)
        {
            return Epoch.AddSeconds(epoch);
        }

        public static DateTime BeiJingNow()
        {
            return DateTime.UtcNow.AddHours(8);
        }
    }
}
