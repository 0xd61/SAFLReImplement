using FlightRadar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.DataAccess
{
    public abstract class IMessageRepository
    {
        public delegate void GetMessageHandler(string message);
        public event GetMessageHandler OnGetMessage;

        protected void NotifyListener(string message)
        {
            OnGetMessage?.Invoke(message);
        }

        public abstract void StartMessageLoop();
    }
}
