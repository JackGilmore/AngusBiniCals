using System;
using System.Collections.Generic;
using GovServiceUtilities.Models.Lookups;

namespace AngusBiniCals.Utilities
{
    // TODO: These payloads can probably be massively simplified
    public static class Payloads
    {
        public static LookupRequestPayload NextBinDaysPayload(string uprn)
        {
            return new LookupRequestPayload()
            {
                FormValues = new Dictionary<string, Dictionary<string, FormValue>>()
                {
                    {
                        "Section 3", new Dictionary<string, FormValue>()
                        {
                            {
                                "serviceUPRN", new FormValue
                                {
                                    Name = "serviceUPRN",
                                    Type = "text",
                                    ValueChanged = true,
                                    SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                                    Label = "UPRN",
                                    HasOther = false,
                                    Value = uprn,
                                    Path = "root/serviceUPRN"
                                }
                            },
                            {
                                "servicePostcode", new FormValue
                                {
                                    Name = "servicePostcode",
                                    Type = "text",
                                    ValueChanged = true,
                                    SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                                    Label = "servicePostcode",
                                    HasOther = false,
                                    Value = "",
                                    Path = "root/servicePostcode"
                                }
                            },
                            {
                                "currentDate", new FormValue
                                {
                                    Name = "currentDate",
                                    Type = "text",
                                    ValueChanged = true,
                                    SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                                    Label = "Current Date",
                                    HasOther = false,
                                    Value = DateTime.Today.ToString("yyyy-MM-dd"),
                                    Path = "root/currentDate"
                                }
                            }
                        }
                    }
                }
            };
        }
        public static LookupRequestPayload CalendarRequestPayload(string uprn, string purpleDate, string greyDate, string greenDate, string brownDate, string blueDate)
        {
            return new LookupRequestPayload()
            {
                FormValues = new Dictionary<string, Dictionary<string, FormValue>>()
                {
                    {
                        "Section 3", new Dictionary<string, FormValue>()
                        {
                            {
                                "serviceUPRN", new FormValue
                                {
                                    Name = "serviceUPRN",
                                    Type = "text",
                                    ValueChanged = true,
                                    SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                                    Label = "UPRN",
                                    HasOther = false,
                                    Value = uprn,
                                    Path = "root/serviceUPRN"
                                }
                            },
                            {
                                "nextPurpleDate", new FormValue
                                {
                                    Name = "nextPurpleDate",
                                    Type = "text",
                                    ValueChanged = true,
                                    SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                                    HasOther = false,
                                    Value = purpleDate,
                                    Path = "root/nextPurpleDate"
                                }
                            },
                            {
                                "nextGreyDate", new FormValue
                                {
                                    Name = "nextGreyDate",
                                    Type = "text",
                                    ValueChanged = true,
                                    SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                                    HasOther = false,
                                    Value = greyDate,
                                    Path = "root/nextGreyDate"
                                }
                            },
                            {
                                "nextGreenDate", new FormValue
                                {
                                    Name = "nextGreenDate",
                                    Type = "text",
                                    ValueChanged = true,
                                    SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                                    HasOther = false,
                                    Value = greenDate,
                                    Path = "root/nextGreenDate"
                                }
                            },
                            {
                                "nextBrownDate", new FormValue
                                {
                                    Name = "nextBrownDate",
                                    Type = "text",
                                    ValueChanged = true,
                                    SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                                    HasOther = false,
                                    Value = brownDate,
                                    Path = "root/nextBrownDate"
                                }
                            },
                            {
                                "nextBlueDate", new FormValue
                                {
                                    Name = "nextBlueDate",
                                    Type = "text",
                                    ValueChanged = true,
                                    SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                                    HasOther = false,
                                    Value = blueDate,
                                    Path = "root/nextBlueDate"
                                }
                            },
                            {
                                "purpleWeekly", new FormValue
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
                            {
                                "greyWeekly", new FormValue
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
                            {
                                "greenWeekly", new FormValue
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
                            {
                                "brownWeekly", new FormValue
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
                            {
                                "blueWeekly", new FormValue
                                {
                                    Name = "blueWeekly",
                                    Type = "text",
                                    ValueChanged = true,
                                    SectionId = "AF-Section-d41ea95d-5aed-4fbd-9338-057ddc7e7c88",
                                    HasOther = false,
                                    Value = "N",
                                    Path = "root/blueWeekly"
                                }
                            },
                        }
                    }
                }
            };
        }
    }
}
