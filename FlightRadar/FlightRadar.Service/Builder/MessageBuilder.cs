using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRadar.Model;
using FlightRadar.Service.MessageParser;

namespace FlightRadar.Service.Builder
{
    /// <summary>
    /// builds messages from ADSB sentence (uses message type to determine which message to build)
    /// </summary>
    public class MessageBuilder : IMessageBuilder
    {
        private delegate ADSBMessageBase BuilderDelegate(string payloadInBin);
        private Dictionary<ADSBMessagetype, BuilderDelegate> builderMethods = new Dictionary<ADSBMessagetype, BuilderDelegate>();

        private IMessageParser parser = null;

        private IPayloadParser payloadParserPosition = null;
        private IPayloadParser payloadParserVelocity = null;
        private IPayloadParser payloadParserIdentification = null;

        /// <summary>
        /// constructor used for dependency injection 
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="position"></param>
        /// <param name="veloctiy"></param>
        /// <param name="identification"></param>
        public MessageBuilder(IMessageParser parser, IPayloadParser position, IPayloadParser veloctiy, IPayloadParser identification)
        {
            this.parser = parser;

            builderMethods.Add(ADSBMessagetype.Position, BuildPositionMessage);
            builderMethods.Add(ADSBMessagetype.Velocity, BuildVelocityMessage);
            builderMethods.Add(ADSBMessagetype.Identification, BuildIdentificationMessage);

            payloadParserPosition = position;
            payloadParserVelocity = veloctiy;
            payloadParserIdentification = identification;

        }

        /// <summary>
        /// delegates to the correct message type
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public ADSBMessageBase BuildMessage(string sentence)
        {
            string message = parser.Parse(sentence);
            string payload = parser.ParsePayload(message).ToBin();
        
            ADSBMessagetype type = parser.ParseMessagetype(payload);

            if (type == ADSBMessagetype.undefined)
                return null;

            return builderMethods[type].Invoke(message);
        }

        /// <summary>
        /// builds position message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private ADSBMessageBase BuildPositionMessage(string message)
        {
            ADSBPositionMessage msg = new ADSBPositionMessage();
            ADSBMessageBase baseMsg = msg as ADSBMessageBase;

            BuildBaseMessage(message, ref baseMsg);
            baseMsg = payloadParserPosition.ParseMessage(baseMsg);
            msg = baseMsg as ADSBPositionMessage;
            msg.TypeSimple = ADSBMessagetype.Position;

            return msg;
        }

        /// <summary>
        /// builds velocity message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private ADSBMessageBase BuildVelocityMessage(string message)
        {
            ADSBVelocityMessage msg = new ADSBVelocityMessage();
            ADSBMessageBase baseMsg = msg as ADSBMessageBase;

            BuildBaseMessage(message, ref baseMsg);
            baseMsg = payloadParserVelocity.ParseMessage(baseMsg);
            msg = baseMsg as ADSBVelocityMessage;
            msg.TypeSimple = ADSBMessagetype.Velocity;

            return msg;
        }

        /// <summary>
        /// builds identification message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ADSBMessageBase BuildIdentificationMessage(string message)
        {
            ADSBIdentificationMessage msg = new ADSBIdentificationMessage();
            ADSBMessageBase baseMsg = msg as ADSBMessageBase;

            BuildBaseMessage(message, ref baseMsg);
            baseMsg = payloadParserIdentification.ParseMessage(baseMsg);
            msg = baseMsg as ADSBIdentificationMessage;
            msg.TypeSimple = ADSBMessagetype.Identification;

            return msg;
        }

        /// <summary>
        /// puts base information into the message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="baseMsg"></param>
        private void BuildBaseMessage(string message, ref ADSBMessageBase baseMsg)
        {
            baseMsg.ICAO = parser.ParseIcao(message);
            baseMsg.Timestamp = parser.ParseTimestamp(message);
            baseMsg.Payload = parser.ParsePayload(message).ToBin();
        } 
    }
}
