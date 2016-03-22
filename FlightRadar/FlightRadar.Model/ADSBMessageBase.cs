using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public class ADSBMessageBase
    {
        public string Timestamp { get; private set; } = string.Empty;
        public string ICAO { get; set; } = string.Empty;
        public int Type { get; set; }
        public int DownlinkFormat { get; set; }
        public int Capability { get; set; }
        public int Payload { get; set; }

        public ADSBMessageBase (string Timestamp, string ICAO, int Type, int DownlinkFormat, int Capability, int Payload)
        {
            this.Timestamp = Timestamp;
            this.ICAO = ICAO;
            this.Type = Type;
            this.DownlinkFormat = DownlinkFormat;
            this.Capability = Capability;
            this.Payload = Payload;
        }
    }
}
