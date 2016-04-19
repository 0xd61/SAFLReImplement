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
    /// <summary>
    /// 
    /// </summary>
    public class MessageViewModel : IDisposable
    {
        private IMessageService messageService = null;
        private IMessageBuilder messageBuilder = null;
        private IMessageRepository messageRepository = null;

        public PlaneContainer Planes { get; private set; } = null;
        public bool IsConntected
        {
            get
            {
                return messageRepository.Connected;
            }
        }

        public MessageViewModel()
        {
            CreateMessageBuilder();
            CreateMessageRepository();
            CreateMessageService();
            CreateContainers();
        }

        public void Update()
        {
            if (IsConntected)
            {
                string msg = messageService.PopRawMessage();

                ADSBMessageBase parsedMessage = GetMessage(msg);

                AddPlane(parsedMessage);
                AddMessageToPlane(parsedMessage);
            }
        }

        private void CreateMessageBuilder()
        {
            messageBuilder = ServiceFactory.CreateMessageBuilderService(
             ServiceFactory.CreateMessageParserService(),
             ServiceFactory.CreatePayloadParserPosition(),
             ServiceFactory.CreatePayloadParserVelocity(),
             ServiceFactory.CreatePayloadParserIdentification());
        }

        private void CreateMessageRepository()
        {
            messageRepository = ServiceFactory.CreateWebRepository("http://flugmon-it.hs-esslingen.de/subscribe/ads.sentence");
        }

        private void CreateMessageService()
        {
            messageService = ServiceFactory.CreateMessageService(messageRepository);
        }

        private void CreateContainers()
        {
            Planes = new PlaneContainer();
        }

        private void AddMessageToPlane(ADSBMessageBase parsedMessage)
        {
            if (!IsMessageValid(parsedMessage))
                return;
            Planes[parsedMessage.ICAO].addMessageToPlane(parsedMessage);
        }

        private void AddPlane(ADSBMessageBase parsedMessage)
        {
            if (IsMessageValid(parsedMessage) && !Planes.ContainsKey(parsedMessage.ICAO))
                Planes.Add(parsedMessage.ICAO, new Plane(parsedMessage.ICAO));
        }

        private bool IsMessageValid(ADSBMessageBase parsedMessage)
        {
            return parsedMessage != null;
        }

        private ADSBMessageBase GetMessage(string message)
        {
            return messageBuilder.BuildMessage(message);
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
