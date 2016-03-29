using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    class PlanePosition
    {
        public DateTime PositionTimestamp { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public double Altitude { get; }

        public PlanePosition(DateTime Timestamp, double Latitude, double Longitude, double Altitude)
        {
            this.PositionTimestamp = Timestamp;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Altitude = Altitude;
        }
    }
}
