using FlightRadar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.DataAccess
{
    public interface IMessageRepository
    {
        ADSBMessageBase GetMessage();
    }
}
