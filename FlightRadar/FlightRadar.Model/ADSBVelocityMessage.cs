using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public class ADSBVelocityMessage : ADSBMessageBase
    {
        public int Subtype { get; set; }
        public int IntentChange { get; set; }
        public int ReservedA { get; set; }
        public int NavigationAccuracy { get; set; }
        public int Speed { get; set; }
        public int Heading { get; set; }
        public int VerticalRateSource { get; set; }
        public int VerticalSpeed { get; set; }

        public ADSBVelocityMessage()
        {
        }
    }
}
