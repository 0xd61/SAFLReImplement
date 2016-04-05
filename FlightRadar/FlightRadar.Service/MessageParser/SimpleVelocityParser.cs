using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRadar.Model;

namespace FlightRadar.Service.MessageParser
{
    public class SimpleVelocityParser : IPayloadParser
    {
        public ADSBMessageBase ParseMessage(ADSBMessageBase message)
        {
            ADSBVelocityMessage tmpMessage = (ADSBVelocityMessage)message;

            return tmpMessage;
        }
    }
}
