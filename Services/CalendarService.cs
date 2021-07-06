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

            var dates = await GetDatesForUPRN(uprn);

            var events = dates.Select(x =>
                new CalendarEvent
                {
                    IsAllDay = true,
                    Start = new CalDateTime(x.Date),
                    End = new CalDateTime(x.Date),
                    Summary = x.Bin
                }
            );

            calendar.Events.AddRange(events);

            return calendar;
        }
    }
}
