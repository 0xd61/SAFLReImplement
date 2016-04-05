using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public class PlanePosition
    {
        public DateTime PositionTimestamp { public get; }
        public double Latitude { public get; }
        public double Longitude { public get; }
        public double Altitude { public get; }

        public PlanePosition(DateTime Timestamp, double Latitude, double Longitude, double Altitude)
        {
            this.PositionTimestamp = Timestamp;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Altitude = Altitude;
        }
    }
}
