namespace AngusBiniCals.Models
{
    public class AddressEntry
    {
        public string Address { get; set; }
        public long UPRN { get; set; }
        public double Easting { get; set; }
        public double Northing { get; set; }
        public string Envelope { get; set; }
        // Get the address but chop off the full stop at the end
        public string AddressTrimmed => Address[..^1];
    }
}
