using System;
using FlightRadar.Model;

namespace FlightRadar.Service.MessageParser
{
    /// <summary>
    /// Parses the position information from the payload
    /// </summary>
    public class SimplePositionParser : IPayloadParser
    {
        /// <summary>
        /// Assembles the message with parsed data
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Parses the Surveillance Status (bit 5-6) from binary payload
        /// </summary>
        /// <param name="payloadInBin"></param>
        /// <returns></returns>
        private int ParseSurveillanceStatus(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(5, 2), 2);
        }

        /// <summary>
        /// Parses the Nic Supplement (bit 7) from binary payload
        /// </summary>
        /// <param name="payloadInBin"></param>
        /// <returns></returns>
        private int ParseNicSupplement(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(7, 1), 2);
        }

        /// <summary>
        /// Parses the Altitude (bit 8-19) from binary payload
        /// </summary>
        /// <param name="payloadInBin"></param>
        /// <returns></returns>
        private int ParseAltitude (string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(8, 12), 2);
        }

        /// <summary>
        /// Parses the Time Flag (bit 20) from binary payload
        /// </summary>
        /// <param name="payloadInBin"></param>
        /// <returns></returns>
        private int ParseTimeFlag(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(20, 1), 2);
        }

        /// <summary>
        /// Parses the Cpr Format (bit 21) from binary payload
        /// </summary>
        /// <param name="payloadInBin"></param>
        /// <returns></returns>
        private int ParseCprFormate(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(21, 1), 2);
        }

        /// <summary>
        /// Parses the Cpr Longitude (bit 22-38) from binary payload
        /// </summary>
        /// <param name="payloadInBin"></param>
        /// <returns></returns>
        private int ParseCprLongitude(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(22, 17), 2);
        }

        /// <summary>
        /// Parses the Cpr Latitude (bit 39-55) from binary payload
        /// </summary>
        /// <param name="payloadInBin"></param>
        /// <returns></returns>
        private int ParseCprLatitude(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(39, 17), 2);
        }

    }
}
