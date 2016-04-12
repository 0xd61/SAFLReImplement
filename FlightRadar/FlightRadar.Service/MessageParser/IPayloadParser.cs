using FlightRadar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Service.MessageParser
{
    public interface IPayloadParser
    {
        ADSBMessageBase ParseMessage(ADSBMessageBase message);
    }
}
