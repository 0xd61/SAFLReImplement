using FlightRadar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Service.Builder
{
    public interface IMessageBuilder
    {
        ADSBMessageBase BuildMessage(string sentence);

    }
}
