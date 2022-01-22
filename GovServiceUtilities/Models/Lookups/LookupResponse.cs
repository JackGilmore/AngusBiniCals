using System.Text.Json.Serialization;

namespace GovServiceUtilities.Models.Lookups
{
    public class LookupResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("integration")]
        public IntegrationResponse Integration { get; set; }

    }

    public class IntegrationResponse
    {
        [JsonPropertyName("transformed")] 
        public TransformedIntegrationResponse TransformedResponse { get; set; }
    }

    public class TransformedIntegrationResponse
    {
        [JsonPropertyName("rows_data")] 
        public Dictionary<string,Dictionary<string,string>> RowsData { get; set; }
    }
}
