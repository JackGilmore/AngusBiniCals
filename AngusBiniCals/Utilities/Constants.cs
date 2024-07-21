using System.Collections.Generic;

namespace AngusBiniCals.Utilities
{
    public static class Constants
    {
        public const string SearchUrl = "https://angus.locationcentre.co.uk/internet/search.ashx?";

        public const string GovServiceUrl = "https://anguscouncil-self.achieveservice.com/";

        public const string NextBinDaysIntegrationId = "66587d491feab";

        public const string BinCalIntegrationId = "66599c39c4c55";

        public static readonly Dictionary<string, string> BinColours = new()
        {
            { "Recycling", "♻ Grey bin" },
            { "Recycling Waste", "♻ Grey bin" },
            { "Plastic bottles and containers, cans and cartons", "♻ Grey bin" },
            { "General Waste", "🗑 Purple bin" },
            { "Non-recyclable Waste", "🗑 Purple bin" },
            { "Food Waste", "🥑 Brown caddy" },
            { "Food", "🥑 Brown caddy" },
            { "Garden Waste (Subscription Only)", "🍂 Green bin" },
            { "Garden Waste", "🍂 Green bin" },
            {"Paper and card", "📝 Blue bin" }
        };

        public static readonly Dictionary<string, string> BinClasses = new()
        {
            { "Recycling", "lgi-bin-recycling" },
            { "Recycling Waste", "lgi-bin-recycling" },
            { "Plastic bottles and containers, cans and cartons", "lgi-bin-recycling" },
            { "General Waste", "lgi-bin-general" },
            { "Non-recyclable Waste", "lgi-bin-general" },
            { "Food Waste", "lgi-bin-food" },
            { "Garden Waste (Subscription Only)", "lgi-bin-garden" },
            { "Food / Garden Waste", "lgi-bin-food-garden" },
            { "Food Waste / Garden Waste", "lgi-bin-food-garden" },
            {"Paper and card", "lgi-bin-paper" }
        };
    }
}
