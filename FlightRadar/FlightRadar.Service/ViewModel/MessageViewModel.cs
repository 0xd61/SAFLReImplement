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
        private IMessageService messageService = null;
        private IMessageBuilder messageBuilder = null;
        private PlaneContainer planes = null;

        public MessageViewModel()
        {
            messageService = new MessageService(new WebMessageRepository("http://flugmon-it.hs-esslingen.de/subscribe/ads.sentence"));
            messageBuilder = new MessageBuilder(new SimpleMessageParser());
            planes = new PlaneContainer();
        }

        public void Update()
        {
            string msg = messageService.PopRawMessage();

            if (msg != string.Empty)
            {
                ADSBMessageBase parsedMessage = messageBuilder.BuildMessage(msg);
                if (parsedMessage == null)
                    return;

                if (!planes.ContainsKey(parsedMessage.ICAO))
                    planes.Add(parsedMessage.ICAO, new Plane(parsedMessage.ICAO));
                Console.WriteLine(parsedMessage.ToString());
                planes[parsedMessage.ICAO].addMessageToPlane(parsedMessage);
            }

        }

        public void Dispose()
        {
            messageService?.Dispose();
        }
    }
}
