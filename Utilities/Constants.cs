using System.Collections.Generic;

namespace AngusBiniCals.Utilities
{
    public static class Constants
    {
        public static readonly string SearchURL = "https://angus.locationcentre.co.uk/internet/search.ashx?";

        public static readonly string CalendarURL = "http://archive.angus.gov.uk/calendarJuly2016/bincalendar.cfm?printtype=2";

        public static readonly Dictionary<string, string> BinColours = new()
        {
            { "Recycling", "♻ Grey bin" },
            { "General Waste", "🗑 Purple bin" },
            { "Food Waste", "🥑 Brown caddy" },
            { "Garden Waste (Subscription Only)", "🍂 Green bin" },
        };
    }
}
