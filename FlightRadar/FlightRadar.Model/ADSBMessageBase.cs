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
        public string ICAO { get; private set; } = string.Empty;
        public int Type { get; private set; }
        public int DownlinkFormat { get; private set; }
        public int Capability { get; private set; }
        public int Payload { get; private set; }

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
