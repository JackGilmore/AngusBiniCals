using System;
using System.Globalization;
using System.Linq;

namespace AngusBiniCals.Models
{
    public class CalendarEntry
    {
        public DateTime Date { get; set; }
        public string Bin { get; set; }

        public CalendarEntry(string dayAndBin, string monthYear)
        {
            var splitDayAndBin = dayAndBin.Split("-").Select(x => x.Trim()).ToArray();

            if (splitDayAndBin.Length != 2)
            {
                throw new Exception($"Unexpected split count when parsing {typeof(CalendarEntry)}");
            }

            var dayNumber = splitDayAndBin[0].Split(" ")[1];

            var mungedDate = $"{dayNumber} {monthYear}";

            var dateResult = DateTime.TryParseExact(mungedDate, "dd MMMM yyyy",CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces, out var dateTime);

            if (!dateResult)
            {
                throw new Exception($"Could not parse {typeof(DateTime)} from string: {mungedDate}");
            }

            Date = dateTime;

            Bin = splitDayAndBin[1];
        }
    }
}
