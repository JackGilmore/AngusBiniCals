using System.Collections.Generic;

namespace AngusBiniCals.Utilities
{
    public static class Constants
    {
        public const string SearchUrl = "https://angus.locationcentre.co.uk/internet/search.ashx?";

        public const string GovServiceUrl = "https://anguscouncil-self.achieveservice.com/";

        public const string NextBinDaysIntegrationId = "605cb3ec1f183";

        public const string BinCalIntegrationId = "61a74a140f9e9";

        public static readonly Dictionary<string, string> BinColours = new()
        {
            { "Recycling", "♻ Grey bin" },
            { "Recycling Waste", "♻ Grey bin" },
            { "General Waste", "🗑 Purple bin" },
            { "Food Waste", "🥑 Brown caddy" },
            { "Food", "🥑 Brown caddy" },
            { "Garden Waste (Subscription Only)", "🍂 Green bin" },
            { "Garden Waste", "🍂 Green bin" }
        };

        public static readonly Dictionary<string, string> BinClasses = new()
        {
            { "Recycling", "lgi-bin-recycling" },
            { "Recycling Waste", "lgi-bin-recycling" },
            { "General Waste", "lgi-bin-general" },
            { "Food Waste", "lgi-bin-food" },
            { "Garden Waste (Subscription Only)", "lgi-bin-garden" },
            { "Food / Garden Waste", "lgi-bin-food-garden" }
        };
    }
}
