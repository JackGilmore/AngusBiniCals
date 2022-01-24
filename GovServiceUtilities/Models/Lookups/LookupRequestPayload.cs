﻿using System.Text.Json.Serialization;

namespace GovServiceUtilities.Models.Lookups;

public class LookupRequestPayload
{
    [JsonPropertyName("stopOnFailure")]
    public bool StopOnFailure { get; set; }

    [JsonPropertyName("formId")]
    public string FormId { get; set; }
    [JsonPropertyName("formValues")]
    public Dictionary<string, Dictionary<string, FormValue>> FormValues { get; set; }
    [JsonPropertyName("isPublished")]
    public bool IsPublished { get; set; }
    [JsonPropertyName("formName")]
    public string FormName { get; set; }
    [JsonPropertyName("tokens")]
    public Dictionary<string, string> Tokens { get; set; }
    [JsonPropertyName("site")]
    public string Site { get; set; }
    [JsonPropertyName("created")]
    public string Created { get; set; }
    [JsonPropertyName("reference")]
    public string Reference { get; set; }
    [JsonPropertyName("formUri")]
    public string FormUri { get; set; }

    public LookupRequestPayload()
    {
        //User = new object();
        FormId = "";
        FormName = "";
        Site = "";
        Created = "";
        Reference = "";
        FormUri = "";
        StopOnFailure = true;
        FormValues = new Dictionary<string, Dictionary<string, FormValue>>()
        {
            {
                "Section 3", new Dictionary<string, FormValue>() {
                    { "serviceUPRN", new FormValue
                        {
                            Name = "serviceUPRN",
                            Type = "text",
                            ValueChanged = true,
                            SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                            Label = "UPRN",
                            HasOther = false,
                            Value = "117133615",
                            Path = "root/serviceUPRN"
                        }
                    },
                    { "nextPurpleDate", new FormValue
                        {
                            Name = "nextPurpleDate",
                            Type = "text",
                            ValueChanged = true,
                            SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                            HasOther = false,
                            Value = "2022-01-27",
                            Path = "root/nextPurpleDate"
                        }
                    },
                    { "nextGreyDate", new FormValue
                        {
                            Name = "nextGreyDate",
                            Type = "text",
                            ValueChanged = true,
                            SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                            HasOther = false,
                            Value = "2022-02-03",
                            Path = "root/nextGreyDate"
                        }
                    },
                    { "nextGreenDate", new FormValue
                        {
                            Name = "nextGreenDate",
                            Type = "text",
                            ValueChanged = true,
                            SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                            HasOther = false,
                            Value = "2022-02-21",
                            Path = "root/nextGreenDate"
                        }
                    },
                    { "nextBrownDate", new FormValue
                        {
                            Name = "nextBrownDate",
                            Type = "text",
                            ValueChanged = true,
                            SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                            HasOther = false,
                            Value = "2022-01-24",
                            Path = "root/nextBrownDate"
                        }
                    },
                    { "purpleWeekly", new FormValue
                        {
                            Name = "purpleWeekly",
                            Type = "text",
                            ValueChanged = true,
                            SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                            HasOther = false,
                            Value = "N",
                            Path = "root/purpleWeekly"
                        }
                    },
                    { "greyWeekly", new FormValue
                        {
                            Name = "greyWeekly",
                            Type = "text",
                            ValueChanged = true,
                            SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                            HasOther = false,
                            Value = "N",
                            Path = "root/greyWeekly"
                        }
                    },
                    { "greenWeekly", new FormValue
                        {
                            Name = "greenWeekly",
                            Type = "text",
                            ValueChanged = true,
                            SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                            HasOther = false,
                            Value = "N",
                            Path = "root/greenWeekly"
                        }
                    },
                    { "brownWeekly", new FormValue
                        {
                            Name = "brownWeekly",
                            Type = "text",
                            ValueChanged = true,
                            SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                            HasOther = false,
                            Value = "Y",
                            Path = "root/brownWeekly"
                        }
                    },
                }
            }
        };

        IsPublished = false;
        Tokens = new Dictionary<string, string>
        {
            {"port", ""},
            {"host", ""},
            {"site_url", ""},
            {"site_path", ""},
            {"site_origin", ""},
            {"user_agent", ""},
            {"site_protocol", ""},
            {"session_id", ""},
            {"product", ""},
            {"formLanguage", ""},
            {"authenticationType", ""},
            //{"isAuthenticated", true},
            {"api_url", ""},
            {"transactionReference", ""},
            {"transaction_status", ""},
            //{"published", false},
            {"summary", "Bin collection dates"},
            {"description", "Bin collection dates"}
        };
    }
}

public class FormValue
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("value_changed")]
    public bool ValueChanged { get; set; }
    [JsonPropertyName("section_id")]
    public string SectionId { get; set; }
    [JsonPropertyName("label")]
    public string Label { get; set; }
    [JsonPropertyName("hasOther")]
    public bool HasOther { get; set; }
    [JsonPropertyName("value")]
    public string Value { get; set; }
    [JsonPropertyName("path")]
    public string Path { get; set; }
}
