using FlightRadar.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightRadar.Service
{
    public class MessageService : IMessageService
    {
        private IMessageRepository repo = null;
        private Queue<string> rawMessages { get; set; } = new Queue<string>();
        private Thread workerThread;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public MessageService(IMessageRepository repository)
        {
            repo = repository;
            repo.OnGetMessage += Repo_OnGetMessage;

            workerThread = new Thread(repo.StartMessageLoop);
            workerThread.Name = "Message Thread";
            workerThread.Start();

        }

        /// <summary>
        /// Event if the a messages is received from the server
        /// </summary>
        /// <param name="message">The message.</param>
        private void Repo_OnGetMessage(string message)
        {
            rawMessages.Enqueue(message);
            Console.WriteLine(message);
        }

        /// <summary>
        /// Pops the first message in the queue
        /// </summary>
        /// <returns></returns>
        public string PopRawMessage()
        {
            if(repo.Connected == false)
            {

            }


            if (rawMessages.Count != 0)
                return rawMessages.Dequeue();
            else
                return string.Empty;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            repo.StopMessageloop = true;
            workerThread.Join();
        }
    }
}
