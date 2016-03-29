using FlightRadar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightRadar.DataAccess;
using System.Threading;
using FlightRadar.Service.MessageParser;
using FlightRadar.Service.Builder;

namespace FlightRadar.Service.ViewModel
{
    public class MessageViewModel : IDisposable
    {
        public List<ADSBMessageBase> MessageList { get; set; } = new List<ADSBMessageBase>();
        public List<string> RawMessages { get; set; } = new List<string>();

        private IMessageService messageService = null;
        private IMessageParser messageParser = null;
        private IMessageBuilder messageBuilder = null;

        public MessageViewModel()
        {
            messageService = new MessageService(new WebMessageRepository("http://flugmon-it.hs-esslingen.de/subscribe/ads.sentence"));
            messageParser = new SimpleMessageParser();
            messageBuilder = new MessageBuilder(messageParser);
            
        }

        public void Update()
        {
            string msg = messageService.PopRawMessage();

            if(msg != string.Empty)
            {
                messageBuilder.BuildMessage(msg);
                RawMessages.Add(msg);
            }

        }

        public void Dispose()
        {
            messageService?.Dispose();
        }
    }
}
