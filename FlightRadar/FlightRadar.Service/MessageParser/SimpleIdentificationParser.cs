using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRadar.Model;

namespace FlightRadar.Service.MessageParser
{
    public class SimpleIdentificationParser : IPayloadParser
    {
        public ADSBMessageBase ParseMessage(ADSBMessageBase message)
        {
            ADSBIdentificationMessage tmpMessage = (ADSBIdentificationMessage)message;



            return tmpMessage;
        }

        private int ParseAltitude(string payloadInBin)
        {

        }
    }
}
