using FlightRadar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightRadar.DataAccess;
using System.Threading;

namespace FlightRadar.Service.ViewModel
{
    public class MessageViewModel
    {
        /// <summary>
        /// Hält die liste von Nachrichten
        /// </summary>
        public List<ADSBMessageBase> MessageList { get; set; } = new List<ADSBMessageBase>();
        public List<string> RawMessages { get; set; } = new List<string>();

        private IMessageService messageService = null;

        public MessageViewModel()
        {
            messageService = new MessageService(new WebMessageRepository("http://flugmon-it.hs-esslingen.de/subscribe/ads.sentence"));
        }

        /// <summary>
        /// Holt eine Message
        /// </summary>
        public void Update()
        {
            string msg = messageService.PopRawMessage();

            if(msg != string.Empty)
                RawMessages.Add(messageService.PopRawMessage());
        }
    }
}
