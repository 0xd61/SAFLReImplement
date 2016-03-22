using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public class ADSBMessageBase
    {
        private string timestamp { get; set; } = string.Empty;
        private string ICAO { get; set; } = string.Empty;
        private int type { get; set; }
        private int downlinkFormat { get; set; }
        private int capability { get; set; }
        private int payload { get; set; }

        public ADSBMessageBase (string timestamp, string ICAO, int type, int downlinkFormat, int capability, int payload)
        {
            this.timestamp = timestamp;
            this.ICAO = ICAO;
            this.type = type;
            this.downlinkFormat = downlinkFormat;
            this.capability = capability;
            this.payload = payload;
        }
    }
}
