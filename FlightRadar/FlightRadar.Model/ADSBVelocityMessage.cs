using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public class ADSBVelocityMessage : ADSBMessageBase
    {
        public int Subtype { get; private set; }
        public int IntentChange { get; private set; }
        public int ReservedA { get; private set; }
        public int NavigationAccuracy { get; private set; }
        public int Speed { get; private set; }
        public int Heading { get; private set; }
        public int VerticalRateSource { get; private set; }
        public int VerticalSpeed { get; private set; }

        public ADSBVelocityMessage( int Subtype,
                                    int IntentChange, int ReservedA,
                                    int NavigationAccuracy, int Speed,
                                    int Heading, int VerticalRateSource,
                                    int VerticalSpeed)
        {
            this.Subtype = Subtype;
            this.IntentChange = IntentChange;
            this.ReservedA = ReservedA;
            this.NavigationAccuracy = NavigationAccuracy;
            this.Speed = Speed;
            this.Heading = Heading;
            this.VerticalRateSource = VerticalRateSource;
            this.VerticalSpeed = VerticalSpeed;
        }
    }
}
