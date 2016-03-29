using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    class Plane
    {
        public string ICAO { get; set; } = string.Empty;
        public ADSBMessageContainer Messages = new ADSBMessageContainer();
    }
}
