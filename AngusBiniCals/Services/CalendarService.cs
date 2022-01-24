using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngusBiniCals.Models;
using AngusBiniCals.Utilities;
using GovServiceUtilities;
using GovServiceUtilities.Models.Lookups;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;

namespace AngusBiniCals.Services
{
    public class CalendarService
    {
        private readonly IClient _govServiceClient;

        public CalendarService()
        {
            _govServiceClient = new Client(Constants.GovServiceUrl);
            _govServiceClient.Init();
        }

        public async Task<IEnumerable<CalendarEntry>> GetDatesForUPRN(string uprn)
        {
            // TODO: Perform initial lookup request to get next recent bin dates to pass to second lookup
            var result = await _govServiceClient.RequestLookup("61a74a140f9e9");

            var content = result.Integration.TransformedResponse.RowsData.First().Value
                .GetValueOrDefault("dateCalHTML");

            var document = new HtmlDocument();
            document.LoadHtml(content);

            var nodes = document.QuerySelectorAll("b , li").OrderBy(node => node.StreamPosition);

            var dates = new List<CalendarEntry>();

            string currentMonthYear = string.Empty;

            foreach (var node in nodes)
            {
                if (node.Name == "b")
                {
                   currentMonthYear = node.InnerText;
                }
                else if (node.Name == "li" && !string.IsNullOrEmpty(currentMonthYear))
                {
                    // TODO: Handle cases where dates collect multiple bins (e.g. food/garden) on same day and produce multiple CalendarEntry objects
                    dates.Add(new CalendarEntry(node.InnerText,currentMonthYear) );
                }
            }

            return dates;
        }

        public async Task<Calendar> GetCalendarForUPRN(string uprn, int? reminder = null)
        {
            var calendar = new Calendar
            {
                Scale = "GREGORIAN",
                Method = "PUBLISH"
            };

            var trigger = reminder.HasValue ? new Trigger(TimeSpan.FromHours((double)-reminder)) : null;

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
                            // TODO: Make a common function for generating calendar events
                            events.Add(new CalendarEvent
                            {
                                Uid = $"{date.Date:s}{bin.Trim()}",
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
                            Uid = $"{date.Date:s}{date.Bin}",
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
                        });
                    }
                }
            }

            calendar.Events.AddRange(events);
            return calendar;
        }
    }
}
