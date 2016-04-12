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

        private IPayloadParser positionParser = null;
        private IPayloadParser velocityParser = null;
        private IPayloadParser identificationParser = null;

        private IMessageRepository messageRepository = null;

        public PlaneContainer Planes { get; } = null;

        public bool IsConntected
        {
            get
            {
                return messageRepository.Connected;
            }
        }

        public MessageViewModel()
        {
            messageRepository = ServiceFactory.CreateWebRepository("http://flugmon-it.hs-esslingen.de/subscribe/ads.sentence");
            messageService = ServiceFactory.CreateMessageService(messageRepository);

            messageParser = ServiceFactory.CreateMessageParserService();
            positionParser = ServiceFactory.CreatePayloadParserPosition();
            velocityParser = ServiceFactory.CreatePayloadParserVelocity();
            identificationParser = ServiceFactory.CreatePayloadParserIdentification();

            messageBuilder = ServiceFactory.CreateMessageBuilderService(messageParser, positionParser, velocityParser, identificationParser);

            Planes = new PlaneContainer();
        }

        public void Update()
        {
            if (IsConntected)
            {
                string msg = messageService.PopRawMessage();

                if (msg != string.Empty)
                {
                    ADSBMessageBase parsedMessage = messageBuilder.BuildMessage(msg);
                    if (parsedMessage == null)
                        return;

                    if (!Planes.ContainsKey(parsedMessage.ICAO))
                        Planes.Add(parsedMessage.ICAO, new Plane(parsedMessage.ICAO));

                    //Console.WriteLine(parsedMessage.ToString());


                    Planes[parsedMessage.ICAO].addMessageToPlane(parsedMessage);
                } 
            }

        }
        
        public ADSBPositionMessage GetPositionMessage(string icao)
        {
            ADSBPositionMessage message = (ADSBPositionMessage)Planes[icao].getADSBMessageContainer().FirstOrDefault(e => e.TypeSimple == ADSBMessagetype.Position);

            return message;
        }

        public ADSBVelocityMessage GetVelocityMessage(string icao)
        {
            ADSBVelocityMessage message = (ADSBVelocityMessage)Planes[icao].getADSBMessageContainer().FirstOrDefault(e => e.TypeSimple == ADSBMessagetype.Velocity);

            return message;
        }

        public ADSBIdentificationMessage GetIdentificationMessage(string icao)
        {
            ADSBIdentificationMessage message = (ADSBIdentificationMessage)Planes[icao].getADSBMessageContainer().FirstOrDefault(e => e.TypeSimple == ADSBMessagetype.Identification);

            return message;
        }                               

        public void Dispose()
        {
            messageService?.Dispose();
        }
    }
}
