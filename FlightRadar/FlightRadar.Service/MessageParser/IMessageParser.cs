using FlightRadar.Model;
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

        string ParseTimestamp(string sentence);
        string ParseDfca(string sentence);
        string ParseIcao(string sentence);
        string ParsePayload(string sentence);
        string ParsePartiy(string sentence);
        string ParseMessagetype(string message);
    }
}
