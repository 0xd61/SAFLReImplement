using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Service.MessageParser
{
    public interface IMessageParser
    {
        string Parse(string adsbSentence);
    }
}
