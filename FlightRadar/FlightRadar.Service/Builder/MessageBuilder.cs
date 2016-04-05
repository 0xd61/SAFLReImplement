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

        public MessageBuilder(IMessageParser parser)
        {
            this.parser = parser;

            builderMethods.Add(ADSBMessagetype.Position, BuildPositionMessage);
            builderMethods.Add(ADSBMessagetype.Velocity, BuildVelocityMessage);
            builderMethods.Add(ADSBMessagetype.Identification, BuildIdentificationMessage);

        }

        public ADSBMessageBase BuildMessage(string sentence)
        {

            BuildIdentificationMessage(sentence);
            string message = parser.Parse(sentence);
            string payload = parser.ParsePayload(message);
            StringBuilder sb = new StringBuilder();

            foreach (char c in payload)
                sb.Append(Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')); //TODO: Besser  machen!
            string payloadInBin = sb.ToString();

            ADSBMessagetype type = parser.ParseMessagetype(payloadInBin);

            if (type == ADSBMessagetype.undefined)
                return null;

            return builderMethods[type].Invoke(payloadInBin);

        }

        private ADSBMessageBase BuildPositionMessage(string message)
        {
            ADSBPositionMessage msg = new ADSBPositionMessage();
            ADSBMessageBase baseMsg = msg as ADSBMessageBase;
            BuildBaseMessage(message, ref baseMsg);
            msg = baseMsg as ADSBPositionMessage;
            return msg;
        }

        private ADSBMessageBase BuildVelocityMessage(string message)
        {
            ADSBVelocityMessage msg = new ADSBVelocityMessage();
            ADSBMessageBase baseMsg = msg as ADSBMessageBase;
            BuildBaseMessage(message, ref baseMsg);
            msg = baseMsg as ADSBVelocityMessage;
            return msg;
        }

        public ADSBMessageBase BuildIdentificationMessage(string message)
        {
            ADSBIdentificationMessage msg = new ADSBIdentificationMessage();
            ADSBMessageBase baseMsg = msg as ADSBMessageBase;
            BuildBaseMessage(message, ref baseMsg);
            msg = baseMsg as ADSBIdentificationMessage;
            return msg;
        }

        private void BuildBaseMessage(string message, ref ADSBMessageBase baseMsg)
        {
            baseMsg.ICAO = parser.ParseIcao(message);
            baseMsg.Timestamp = parser.ParseTimestamp(message);
            baseMsg.Payload = parser.ParsePayload(message);

        } 
    }
}
