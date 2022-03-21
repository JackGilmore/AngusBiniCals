using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AngusBiniCals.Utilities;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

namespace AngusBiniCals.Models;

public class CalendarEntry
{
    public DateTime Date { get; }
    public string Bin { get; }

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

    public IEnumerable<CalendarEvent> ToCalendarEvent(Trigger trigger)
    {
        var result = new List<CalendarEvent>();
        if (Bin.Contains('/'))
        {
            var splitBins = Bin.Split("/");

            result.AddRange(splitBins.Select(bin => GenerateCalendarEvent(bin, Date, trigger)));
        }
        else
        {
            result.Add(GenerateCalendarEvent(Bin,Date,trigger));
        }
        return result;
    }

    private static CalendarEvent GenerateCalendarEvent(string bin, DateTime date, Trigger trigger)
    {
        var binColourResult = Constants.BinColours.TryGetValue(bin.Trim(), out var binColour);

        if (binColourResult)
        {
            return new CalendarEvent
            {
                Uid = $"{date:s}{bin.Trim()}",
                IsAllDay = true,
                Start = new CalDateTime(date.Date),
                End = new CalDateTime(date.Date),
                Summary = binColour,
                Alarms = { trigger != null ? new Alarm
                {
                    Description = $"Put out the {binColour} for collection",
                    Action = AlarmAction.Display,
                    Trigger = trigger
                } : null}
            };
        }

        return null;
    }
}