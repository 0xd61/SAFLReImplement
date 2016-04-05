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
    public static class ServiceFactory
    {
        public static IMessageService CreateMessageService(IMessageRepository repository)
        {
            return new MessageService(repository);
        }

        public static IMessageParser CreateMessageParserService()
        {
            return new SimpleMessageParser();
        }

        public static IMessageBuilder CreateMessageBuilderService(IMessageParser messageParser)
        {
            return new MessageBuilder(messageParser);
        }
    }
}
