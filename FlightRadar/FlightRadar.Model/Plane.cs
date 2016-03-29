using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    class Plane
    {
        public string ICAO { get; } = string.Empty;
        public ADSBMessageContainer Messages { get; } = new ADSBMessageContainer();

        public Plane(string ICAO)
        {
            this.ICAO = ICAO;
        }
    }
}
