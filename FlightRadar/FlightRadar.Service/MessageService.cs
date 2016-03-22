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

        public MessageService(IMessageRepository repository)
        {
            repo = repository;
            repo.OnGetMessage += Repo_OnGetMessage;

            Thread workerThread = new Thread(repo.StartMessageLoop);
            workerThread.Start();

            //TODO: Den Thread wieder stoppen sdg
        }

        private void Repo_OnGetMessage(string message)
        {
            rawMessages.Enqueue(message);
            Console.WriteLine(message);
        }

        public string PopRawMessage()
        {
            if(rawMessages.Count != 0)
                return rawMessages.Dequeue();
            else
                return string.Empty;
        }
    }
}
