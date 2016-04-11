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
        private IMessageParser messageParser = null;
        private IMessageRepository messageRepository = null;

        public PlaneContainer Planes { get; } = null;

        public MessageViewModel()
        {
            messageRepository = ServiceFactory.CreateWebRepository("http://flugmon-it.hs-esslingen.de/subscribe/ads.sentence");
            messageService = ServiceFactory.CreateMessageService(messageRepository);

            messageParser = ServiceFactory.CreateMessageParserService();
            messageBuilder = ServiceFactory.CreateMessageBuilderService(messageParser);

            Planes = new PlaneContainer();
        }

        public void Update()
        {
            string msg = messageService.PopRawMessage();

            if (msg != string.Empty)
            {
                ADSBMessageBase parsedMessage = messageBuilder.BuildMessage(msg);
                if (parsedMessage == null)
                    return;

                if (!Planes.ContainsKey(parsedMessage.ICAO))
                    Planes.Add(parsedMessage.ICAO, new Plane(parsedMessage.ICAO));
                Console.WriteLine(parsedMessage.ToString());
                Planes[parsedMessage.ICAO].addMessageToPlane(parsedMessage);
            }

        }

        public void Dispose()
        {
            messageService?.Dispose();
        }
    }
}
