using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngusBiniCals.Models
{
    public class AddressEntry
    {
        public string Address { get; set; }
        public long UPRN { get; set; }
        public double Easting { get; set; }
        public double Northing { get; set; }
        public string Envelope { get; set; }

    }
}
