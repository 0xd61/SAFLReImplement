using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Service
{
    /// <summary>
    /// CPR Berechnung aus 2 Messages 
    /// </summary>
    class CPRCoder
    {
        public readonly int NZ = 15; //Latitude Zones
        public readonly double Dlat0 = 360 / 60.0; //EVEN Message (Latitude zone size NORTH/SOUTH)
        public readonly double Dlat1 = 360 / 59.0; //ODD Message (Latitude zone size NORTH/SOUTH)
        public readonly double Nb17 = 131072.0; //Number of bits for Encoding

        
    }
}
