using FlightRadar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightRadar.DataAccess;
using System.Threading;
using FlightRadar.Service.MessageParser;

namespace FlightRadar.Service.ViewModel
{
    public class MessageViewModel : IDisposable
    {
        public List<ADSBMessageBase> MessageList { get; set; } = new List<ADSBMessageBase>();
        public List<string> RawMessages { get; set; } = new List<string>();

        private IMessageService messageService = null;
        private IMessageParser messageParser = null;

        public MessageViewModel()
        {
            messageService = new MessageService(new WebMessageRepository("http://flugmon-it.hs-esslingen.de/subscribe/ads.sentence"));
            messageParser = new SimpleMessageParser();
        }

        public void Update()
        {
            string msg = messageService.PopRawMessage();

            if(msg != string.Empty)
            {
                RawMessages.Add(messageParser.Parse(msg));
            }

        }

        public void Dispose()
        {
            messageService?.Dispose();
        }
    }
}
