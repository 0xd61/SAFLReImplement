using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public enum ADSBMessagetype { Identification, Position, Velocity }
    public class ADSBMessageBase
    {
        public string Timestamp { get; set; } = string.Empty;
        public string ICAO { get; set; } = string.Empty;
        public int Type { get; set; }
        public int DownlinkFormat { get; set; }
        public int Capability { get; set; }
        public int Payload { get; set; }

        public ADSBMessageBase ()
        {
        }
    }
}
