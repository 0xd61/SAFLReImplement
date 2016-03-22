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
            string message = parser.Parse(sentence);
            string payloadInBin = Convert.ToString(Convert.ToInt32(message, 16), 2); //TODO:Testen, ob Nullen am Anfang

            ADSBMessagetype type = parser.ParseMessagetype(payloadInBin);
            return builderMethods[type].Invoke(payloadInBin);

        }

        private ADSBMessageBase BuildPositionMessage(string payloadInBin)
        {
            return null;
        }

        private ADSBMessageBase BuildVelocityMessage(string payloadInBin)
        {
            return null;
        }

        private ADSBMessageBase BuildIdentificationMessage(string payloadInBin)
        {
            ADSBPositionMessage msg = new ADSBPositionMessage(0,0,0,0,0,0,0);
            ADSBMessageBase baseMsg = msg as ADSBMessageBase;
            BuildBaseMessage(payloadInBin, ref baseMsg);
            msg = baseMsg as ADSBPositionMessage;
            return null;
        }

        private void BuildBaseMessage(string payloadInBin, ref ADSBMessageBase baseMsg)
        {
        } 
    }
}
