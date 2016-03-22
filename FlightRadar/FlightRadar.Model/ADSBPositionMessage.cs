using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public class ADSBPositionMessage : ADSBMessageBase
    {
        public int SurveillanceStatus { get; private set; }
        public int NicSupplement { get; private set; }
        public int Altitude { get; private set; }
        public int TimeFlag { get; private set; }
        public int CprFormate { get; private set; }
        public int CprLongitude { get; private set; }
        public int CprLatitude { get; private set; }

        public ADSBPositionMessage(int SurveillanceStatus, int NicSupplement, int Altitude, int TimeFlag, int CprFormate, int CprLongitude, int CprLatitude) : base("","",0,0,0,0)
        {
            this.SurveillanceStatus = SurveillanceStatus;
            this.NicSupplement = NicSupplement;
            this.Altitude = Altitude;
            this.TimeFlag = TimeFlag;
            this.CprFormate = CprFormate;
            this.CprLongitude = CprLongitude;
            this.CprLatitude = CprLatitude;
        }
    }
}
