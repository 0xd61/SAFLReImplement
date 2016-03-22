using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlightRadar.Service
{
    class MessageParser : IMessageParser
    {
        public string Parse(string adsbSentence)
        {
            Regex pattern = new Regex(@"\d+\.\d+!ADS-B\*[0-9A-Z]{28}");
            string myString = pattern.Match(adsbSentence).ToString();
            return myString;
        }
    }
}
