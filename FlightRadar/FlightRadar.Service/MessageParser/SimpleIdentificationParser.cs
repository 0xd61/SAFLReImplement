using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRadar.Model;

namespace FlightRadar.Service.MessageParser
{
    /// <summary>
    /// Parses the identification information from the payload
    /// </summary>
    public class SimpleIdentificationParser : IPayloadParser
    {

        public char[] SixBitChar = { '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_', ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?' };

        public ADSBMessageBase ParseMessage(ADSBMessageBase Message)
        {
            ADSBIdentificationMessage TmpMessage = (ADSBIdentificationMessage)Message;

            TmpMessage.EmitterCategory = ParseEmmiterCategory(Message.Payload);
            TmpMessage.AircraftID = ParseAircraftID(Message.Payload);

            return TmpMessage;
        }

        private int ParseEmmiterCategory(String payload)
        {
            return Convert.ToInt32(payload.Substring(5, 3), 2);
        }

        private String ParseAircraftID(String payload)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(SixBitChar[Convert.ToInt32(payload.Substring(8, 6), 2)]);
            sb.Append(SixBitChar[Convert.ToInt32(payload.Substring(14, 6), 2)]);
            sb.Append(SixBitChar[Convert.ToInt32(payload.Substring(20, 6), 2)]);
            sb.Append(SixBitChar[Convert.ToInt32(payload.Substring(26, 6), 2)]);
            sb.Append(SixBitChar[Convert.ToInt32(payload.Substring(32, 6), 2)]);
            sb.Append(SixBitChar[Convert.ToInt32(payload.Substring(38, 6), 2)]);
            sb.Append(SixBitChar[Convert.ToInt32(payload.Substring(44, 6), 2)]);
            sb.Append(SixBitChar[Convert.ToInt32(payload.Substring(50, 6), 2)]);

            return sb.ToString();
        }


    }
}
