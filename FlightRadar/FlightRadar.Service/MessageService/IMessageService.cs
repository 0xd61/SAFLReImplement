using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Service
{
    public interface IMessageService : IDisposable
    {

        /// <summary>
        /// Pops the first message in the queue
        /// </summary>
        /// <returns></returns>
        string PopRawMessage();
    }
}
