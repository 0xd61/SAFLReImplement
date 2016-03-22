using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Service
{
    public interface IMessageParser
    {
        void Parse(string text);
    }
}
