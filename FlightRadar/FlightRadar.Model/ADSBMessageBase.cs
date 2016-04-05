using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public enum ADSBMessagetype { Identification, Position, Velocity, undefined }
    public class ADSBMessageBase
    {
        public DateTime Timestamp { get; set; }
        public string ICAO { get; set; } = string.Empty;
        public int Type { get; set; }
        public int DownlinkFormat { get; set; }
        public int Capability { get; set; }
        public int Payload { get; set; }

        public ADSBMessageBase ()
        {
        }

        public override string ToString()
        {
            return "ICAO: " + ICAO;
        }
    }
}
