using System;

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
