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
        public ADSBMessagetype TypeSimple { get; set; }
        public int DownlinkFormat { get; set; }
        public int Capability { get; set; }
        /// <summary>
        /// Payload in Binary
        /// </summary>
        public string Payload { get; set; }

        public ADSBMessageBase ()
        {
        }

        public override string ToString()
        {
            return "ICAO: " + ICAO;
        }
    }
}
