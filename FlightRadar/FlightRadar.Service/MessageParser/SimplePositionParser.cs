using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRadar.Model;


//public int SurveillanceStatus { get; set; }
//public int NicSupplement { get; set; }
//public int Altitude { get; set; }
//public int TimeFlag { get; set; }
//public int CprFormate { get; set; }
//public int CprLongitude { get; set; }
//public int CprLatitude { get; set; }

namespace FlightRadar.Service.MessageParser
{
    public class SimplePositionParser : IPayloadParser
    {
        public ADSBMessageBase ParseMessage(ADSBMessageBase message)
        {
            ADSBPositionMessage tmpMessage = (ADSBPositionMessage)message;

            tmpMessage.Altitude = ParseAltitude(message.Payload);

            return tmpMessage;
        }

        private int ParseSurveillanceStatus(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(5, 2), 2);
        }

        private int ParseNicSupplement(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(7, 1), 2);
        }

        private int ParseAltitude (string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(8, 12), 2);
        }

        private int ParseTimeFlag(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(20, 1), 2);
        }

        private int ParseCprFormate(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(21, 1), 2);
        }

        private int ParseCprLongitude(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(22, 17), 2);
        }

        private int ParseCprLatitude(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(39, 17), 2);
        }

    }
}
