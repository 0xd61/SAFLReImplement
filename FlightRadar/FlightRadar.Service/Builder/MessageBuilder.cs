using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRadar.Model;
using FlightRadar.Service.MessageParser;

namespace FlightRadar.Service.Builder
{
    public class MessageBuilder : IMessageBuilder
    {
        private delegate ADSBMessageBase BuilderDelegate(string payloadInBin);
        private Dictionary<ADSBMessagetype, BuilderDelegate> builderMethods = new Dictionary<ADSBMessagetype, BuilderDelegate>();

        private IMessageParser parser = null;

        private IPayloadParser payloadParserPosition = null;
        private IPayloadParser payloadParserVelocity = null;
        private IPayloadParser payloadParserIdentification = null;


        public MessageBuilder(IMessageParser parser)
        {
            this.parser = parser;

            builderMethods.Add(ADSBMessagetype.Position, BuildPositionMessage);
            builderMethods.Add(ADSBMessagetype.Velocity, BuildVelocityMessage);
            builderMethods.Add(ADSBMessagetype.Identification, BuildIdentificationMessage);

            payloadParserPosition = new SimplePositionParser();
            payloadParserVelocity = new SimpleVelocityParser();
            payloadParserIdentification = new SimpleIdentificationParser();

        }

        public ADSBMessageBase BuildMessage(string sentence)
        {
            string message = parser.Parse(sentence);
            string payload = parser.ParsePayload(message).ToBin();
        
            ADSBMessagetype type = parser.ParseMessagetype(payload);

            if (type == ADSBMessagetype.undefined)
                return null;

            return builderMethods[type].Invoke(message);
        }

        private ADSBMessageBase BuildPositionMessage(string message)
        {
            ADSBPositionMessage msg = new ADSBPositionMessage();
            ADSBMessageBase baseMsg = msg as ADSBMessageBase;

            BuildBaseMessage(message, ref baseMsg);
            baseMsg = payloadParserPosition.ParseMessage(baseMsg);
            msg = baseMsg as ADSBPositionMessage;
            


            return msg;
        }

        private ADSBMessageBase BuildVelocityMessage(string message)
        {
            ADSBVelocityMessage msg = new ADSBVelocityMessage();
            ADSBMessageBase baseMsg = msg as ADSBMessageBase;

            BuildBaseMessage(message, ref baseMsg);
            baseMsg = payloadParserVelocity.ParseMessage(baseMsg);
            msg = baseMsg as ADSBVelocityMessage;




            return msg;
        }

        public ADSBMessageBase BuildIdentificationMessage(string message)
        {
            ADSBIdentificationMessage msg = new ADSBIdentificationMessage();
            ADSBMessageBase baseMsg = msg as ADSBMessageBase;

            BuildBaseMessage(message, ref baseMsg);
            baseMsg = payloadParserIdentification.ParseMessage(baseMsg);
            msg = baseMsg as ADSBIdentificationMessage;

            


            return msg;
        }

        private void BuildBaseMessage(string message, ref ADSBMessageBase baseMsg)
        {
            baseMsg.ICAO = parser.ParseIcao(message);
            baseMsg.Timestamp = parser.ParseTimestamp(message);
            baseMsg.Payload = parser.ParsePayload(message).ToBin();
        } 
    }
}
