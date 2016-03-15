using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRadar.Model;

namespace FlightRadar.DataAccess
{
    public class WebMessageRepository : IMessageRepository
    {
        public string ServerURL { get; private set; } = string.Empty;

        public WebMessageRepository(string url)
        {
            ServerURL = url;
        }

        public ADSBMessageBase GetMessage()
        {
            throw new NotImplementedException();
        }
    }
}
