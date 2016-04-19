using FlightRadar.DataAccess;
using FlightRadar.Service.Builder;
using FlightRadar.Service.MessageParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Service
{
    /// <summary>
    /// Class used to create the services
    /// </summary>
    public static class ServiceFactory
    {
        public static IMessageRepository CreateWebRepository(string url)
        {
            return new WebMessageRepository(url);
        }

        public static IMessageService CreateMessageService(IMessageRepository repository)
        {
            return new MessageService(repository);
        }

        public static IMessageParser CreateMessageParserService()
        {
            return new SimpleMessageParser();
        }

        public static IPayloadParser CreatePayloadParserPosition()
        {
            return new SimplePositionParser();
        }

        public static IPayloadParser CreatePayloadParserVelocity()
        {
            return new SimpleVelocityParser();
        }

        public static IPayloadParser CreatePayloadParserIdentification()
        {
            return new SimpleIdentificationParser();
        }

        public static IMessageBuilder CreateMessageBuilderService(IMessageParser messageParser, IPayloadParser position, IPayloadParser velocity, IPayloadParser identification)
        {
            return new MessageBuilder(messageParser, position, velocity, identification);
        }

    }
}
