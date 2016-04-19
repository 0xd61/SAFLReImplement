using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRadar.Model;

namespace FlightRadar.Service.MessageParser
{
    /// <summary>
    /// Parses the position information from the payload
    /// </summary>
    public class SimplePositionParser : IPayloadParser
    {
        public ADSBMessageBase ParseMessage(ADSBMessageBase message)
        {
            ADSBPositionMessage tmpMessage = (ADSBPositionMessage)message;

            tmpMessage.Altitude = ParseAltitude(message.Payload);
            tmpMessage.SurveillanceStatus = ParseSurveillanceStatus(message.Payload);
            tmpMessage.NicSupplement = ParseNicSupplement(message.Payload);
            tmpMessage.TimeFlag = ParseTimeFlag(message.Payload);
            tmpMessage.CprFormate = ParseCprFormate(message.Payload);
            tmpMessage.CprLatitude = ParseCprLatitude(message.Payload);
            tmpMessage.CprLongitude = ParseCprLongitude(message.Payload);

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
