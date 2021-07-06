using System;
using System.Linq;

namespace AngusBiniCals.Models
{
    public class CalendarEntry
    {
        public DateTime Date { get; set; }
        public string Bin { get; set; }

        public CalendarEntry(string input)
        {
            var splitInput = input.Split("-").Select(x => x.Trim()).ToArray();

            if (splitInput.Length != 2)
            {
                throw new Exception($"Unexpected split count when parsing {typeof(CalendarEntry)}");
            }

            var dateResult = DateTime.TryParse(splitInput[0], out var dateTime);

            if (!dateResult)
            {
                throw new Exception("Could not parse DateTime from string");
            }

            Date = dateTime;

            Bin = splitInput[1];
        }
    }
}
