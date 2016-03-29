using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public class ADSBPositionMessage : ADSBMessageBase
    {
        public int SurveillanceStatus { get; set; }
        public int NicSupplement { get; set; }
        public int Altitude { get; set; }
        public int TimeFlag { get; set; }
        public int CprFormate { get; set; }
        public int CprLongitude { get; set; }
        public int CprLatitude { get; set; }

        public ADSBPositionMessage()
        {
                          
        }
    }
}
