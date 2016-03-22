using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public class ADSBMessageBase
    {
        public string Name { get; set; } = string.Empty;
        public string ICAO { get; set; } = string.Empty;
    }
}
