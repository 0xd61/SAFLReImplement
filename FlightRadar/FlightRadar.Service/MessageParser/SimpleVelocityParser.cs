using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRadar.Model;

namespace FlightRadar.Service.MessageParser
{
    /// <summary>
    /// Parses the velocity information from the payload
    /// </summary>
    public class SimpleVelocityParser : IPayloadParser
    {
        public ADSBMessageBase ParseMessage(ADSBMessageBase message)
        {
            ADSBVelocityMessage tmpMessage = (ADSBVelocityMessage)message;

            tmpMessage.Subtype = ParseSubtype(message.Payload);
            tmpMessage.IntentChange = ParseIntentChange(message.Payload);
            tmpMessage.ReservedA = ParseReservedA(message.Payload);
            tmpMessage.NavigationAccuracy = ParseNaviagationAccuracy(message.Payload);
            tmpMessage.Speed = ParseSpeed(message.Payload);
            tmpMessage.Heading = ParseHeading(message.Payload);
            tmpMessage.VerticalRateSource = ParseVerticalRateSource(message.Payload);
            tmpMessage.VerticalSpeed = ParseVerticalSpeed(message.Payload);

            return tmpMessage;
        }

        private int ParseSubtype(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(5, 3), 2);
        }

        private int ParseIntentChange(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(8, 1), 2);
        }

        private int ParseReservedA(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(9, 1), 2);
        }

        private int ParseNaviagationAccuracy(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(10, 3), 2);
        }

        private int ParseSpeed(string payloadInBin)
        {
            int subtype = ParseSubtype(payloadInBin);
            if (subtype == 1 || subtype == 2)
            {
                int eastWestVelocity = Convert.ToInt32(payloadInBin.Substring(14, 10), 2);
                int northSouthVelocity = Convert.ToInt32(payloadInBin.Substring(25, 10), 2);
                return (int)Math.Sqrt(Math.Pow(eastWestVelocity, 2) + Math.Pow(northSouthVelocity, 2));
            }
            else
            {
                return -1; // subtype 3 / 4 not implemented
            }
        }

        private int ParseHeading(string payloadInBin)
        {
            int subtype = ParseSubtype(payloadInBin);

            if (subtype == 1 || subtype == 2)
            {
                // trigonometry: a = east/west x; b = north/south y; c = velocity; tan(beta) = b / a
                // degrees = atan (b / a) !!! atan is not a continuous function: add a small weight to a 0.00013 !!!
                int eastWestDirection = Convert.ToInt32(payloadInBin.Substring(13, 1), 2);
                int eastWestVelocity = Convert.ToInt32(payloadInBin.Substring(14, 10), 2);
                int northSouthDirection = Convert.ToInt32(payloadInBin.Substring(24, 1), 2);
                int northSouthVelocity = Convert.ToInt32(payloadInBin.Substring(25, 10), 2);
                double deg = (Math.Atan((northSouthVelocity + 0.00013) / (eastWestVelocity + 0.00013))) * 180 / Math.PI;
                if (eastWestDirection == 0 && northSouthDirection == 0)
                {         // NORTH-EAST - QUADRANT I
                    return 90 - (int)deg;
                }
                else if (eastWestDirection == 0 && northSouthDirection == 1)
                { // SOUTH-EAST - QUADRANT II
                    return 90 + (int)deg;
                }
                else if (eastWestDirection == 1 && northSouthDirection == 1)
                { // SOUTH-WEST - QUADRANT III
                    return 270 - (int)deg;
                }
                else
                {                                                           // NORTH-WEST - QUADRANT IV
                    return 270 + (int)deg;
                }
            }
            else
            {
                return -1; // subtype 3 / 4 not implemented

            }
        }

        private int ParseVerticalRateSource(string payloadInBin)
        {
            return Convert.ToInt32(payloadInBin.Substring(35, 1), 2);
        }

        private int ParseVerticalSpeed(string payloadInBin)
        {
            int verticalSign = Convert.ToInt32(payloadInBin.Substring(36, 1), 2);
            int verticalRate = Convert.ToInt32(payloadInBin.Substring(37, 9), 2);
            int rate = (verticalRate - 1) * 64;
            if (verticalSign == 0)
                return rate;        // going up
            else
                return rate * -1;   // going down
        }
    }
}
