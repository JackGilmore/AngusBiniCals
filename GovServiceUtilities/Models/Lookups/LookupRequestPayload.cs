using System.Text.Json.Serialization;

namespace GovServiceUtilities.Models.Lookups;

public class LookupRequestPayload
{
    [JsonPropertyName("formValues")]
    public Dictionary<string, Dictionary<string, FormValue>> FormValues { get; set; } = new();
}

public class FormValue
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    [JsonPropertyName("value_changed")]
    public bool ValueChanged { get; set; }
    [JsonPropertyName("section_id")]
    public string? SectionId { get; set; }
    [JsonPropertyName("label")]
    public string? Label { get; set; }
    [JsonPropertyName("hasOther")]
    public bool HasOther { get; set; }
    [JsonPropertyName("value")]
    public string? Value { get; set; }
    [JsonPropertyName("path")]
    public string? Path { get; set; }
}
