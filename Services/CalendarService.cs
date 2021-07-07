using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngusBiniCals.Models;
using AngusBiniCals.Utilities;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using RestSharp;

namespace AngusBiniCals.Services
{
    public class CalendarService
    {
        public async Task<IEnumerable<CalendarEntry>> GetDatesForUPRN(string uprn)
        {
            var client = new RestClient(Constants.CalendarURL);
            var request = new RestRequest(Method.GET);
            request.AddParameter("uprn", uprn);

            var response = await client.ExecuteAsync(request);

            var document = new HtmlDocument();
            document.LoadHtml(response.Content);

            var datesAsNodes = document.DocumentNode.QuerySelectorAll("h3 + ul li");

            var dates = datesAsNodes.Select(x =>
                new CalendarEntry(
                    x.InnerText
                        .Replace("&nbsp;", "")
                        .Replace("\r", "")
                        .Replace("\n", "")
                    )
            );

            return dates;
        }

        public async Task<Calendar> GetCalendarForUPRN(string uprn)
        {
            var calendar = new Calendar
            {
                Scale = "GREGORIAN",
                Method = "PUBLISH"
            };

            calendar.AddProperty("X-WR-CALNAME", "Bin calendar");
            calendar.AddProperty("X-WR-TIMEZONE", "Europe/London");
            calendar.AddProperty("X-WR-CALDESC", "Bin collection calendar for the Angus council area");

            var dates = await GetDatesForUPRN(uprn);

            var events = new List<CalendarEvent>();

            foreach (var date in dates)
            {
                if (date.Bin.Contains("/"))
                {
                    var splitBins = date.Bin.Split("/");

                    foreach (var bin in splitBins)
                    {
                        var binColourResult = Constants.BinColours.TryGetValue(bin.Trim(), out string binColour);

                        if (binColourResult)
                        {
                            events.Add(new CalendarEvent
                            {
                                IsAllDay = true,
                                Start = new CalDateTime(date.Date),
                                End = new CalDateTime(date.Date),
                                Summary = binColour,
                                Alarms = { new Alarm
                                {
                                    Description = $"Put out the {binColour} for collection",
                                    Action = AlarmAction.Display,
                                    Trigger = new Trigger(TimeSpan.FromHours(-6))
                                }}
                            });
                        }
                    }
                }
                else
                {
                    var binColourResult = Constants.BinColours.TryGetValue(date.Bin, out string binColour);

                    if (binColourResult)
                    {
                        events.Add(new CalendarEvent
                        {
                            IsAllDay = true,
                            Start = new CalDateTime(date.Date),
                            End = new CalDateTime(date.Date),
                            Summary = binColour,
                            Alarms = { new Alarm
                            {
                                Description = $"Put out the {binColour} for collection",
                                Action = AlarmAction.Display,
                                Trigger = new Trigger(TimeSpan.FromHours(-6))
                            }}
                        });
                    }


                }
            }

            calendar.Events.AddRange(events);



            return calendar;
        }
    }
}
