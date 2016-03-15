using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Service
{
    public class SimpleMessageParser : IMessageParser
    {
        public void Parse(string text)
        {
            Console.WriteLine(text);
        }
    }
}
