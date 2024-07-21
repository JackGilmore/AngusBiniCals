using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngusBiniCals.Models;
using AngusBiniCals.Utilities;
using GovServiceUtilities;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Ical.Net;
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
            var nextBinDaysResult = await _govServiceClient.RequestLookup(Constants.NextBinDaysIntegrationId, Payloads.NextBinDaysPayload(uprn));

            var binDays = nextBinDaysResult?
                .Integration.TransformedResponse.RowsData
                .Select(x => x.Value)
                .ToDictionary(x => x["binTypeList"], x => x["binDate"]);

            if (binDays == null)
            {
                throw new NullReferenceException($"Could not retrieve next bin days for UPRN {uprn}");
            }

            var binCalResult = await _govServiceClient.RequestLookup(Constants.BinCalIntegrationId, Payloads.CalendarRequestPayload(uprn, binDays["Purple"], binDays["Grey"], binDays["Green"], binDays["Brown"], binDays["Blue"]));

            if (binCalResult == null)
            {
                throw new NullReferenceException($"Could not retrieve bin calendar for UPRN {uprn}");
            }

            var content = binCalResult.Integration.TransformedResponse.RowsData.First()
                .Value
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
                    dates.Add(new CalendarEntry(node.InnerText, currentMonthYear));
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

            dates.ToList().ForEach(d => calendar.Events.AddRange(d.ToCalendarEvent(trigger)));

            return calendar;
        }
    }
}
