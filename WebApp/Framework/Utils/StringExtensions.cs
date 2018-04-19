using System.Text;
using WebSocketSharp;

namespace Framework.Utils
{
    public static class StringExtensions
    {
        public static string StringArrayToString(this string[] arr)
        {
            if (arr == null) return "";
            var s = new StringBuilder();
            foreach (var d in arr)
            {
                s.Append($"{d} ");
            }
            return s.ToString().Trim();
        }

        public static string[] StringArrayFromString(this string str)
        {
            return str.IsNullOrEmpty() ? new string[] { } : str.Split(' ');
        }

        public static long LongFromString(this string str)
        {
            if (str.IsNullOrEmpty()) return 0;
            long result;
            var parseSuccess = long.TryParse(str, out result);
            return !parseSuccess ? 0 : result;
        }

        public static string ToHumanTimeRange(this string epochTimeRange)
        {
            var timerange = epochTimeRange.Split(' ');
            if (timerange.Length != 2) return "";
            long from, to;
            var parseFromSucces = long.TryParse(timerange[0], out from);
            var parseToSucces = long.TryParse(timerange[1], out to);
            if (!parseFromSucces || !parseToSucces) return "";
            var fromFormat = from.DateTimeFromEpoch().Minute >= 10 ? "" : "0";
            var toFormat = to.DateTimeFromEpoch().Minute >= 10 ? "" : "0";
            return $"{from.DateTimeFromEpoch().Hour}:{fromFormat}{from.DateTimeFromEpoch().Minute}-{to.DateTimeFromEpoch().Hour}:{toFormat}{to.DateTimeFromEpoch().Minute}";
        }
    }
}