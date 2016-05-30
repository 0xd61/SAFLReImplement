using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightRadar.Service;
using FlightRadar.Model;
using FlightRadar.Service.MessageParser;

namespace FlightRadarComponentTest
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void PayloadParser_Identification()
        {
            string adsbSentence = "1379574427.9127481!ADS-B*8D4840D6202CC371C32CE0576098;";
            IPayloadParser parser = new SimpleIdentificationParser();
            IMessageParser payloadParser = new SimpleMessageParser();
            ADSBIdentificationMessage message = new ADSBIdentificationMessage();
            message.Payload = payloadParser.ParsePayload(adsbSentence).ToBin();

            message = parser.ParseMessage(message) as ADSBIdentificationMessage;

            Assert.Equals(message.AircraftID, "KLM1023");
            //Assert.Equals(message.EmitterCategory, 1337);
        }

        [TestMethod]
        public void PayloadParser_Position()
        {
            string adsbSentence = "1379574427.9127481!ADS-B*8D40621D58C382D690C8AC2863A7;";
            IPayloadParser parser = new SimplePositionParser();
            IMessageParser payloadParser = new SimpleMessageParser();
            ADSBPositionMessage message = new ADSBPositionMessage();
            message.Payload = payloadParser.ParsePayload(adsbSentence).ToBin();
            message = parser.ParseMessage(message) as ADSBPositionMessage;

            //Assert.Equals(message.NicSupplement, "1111");
            Assert.Equals(message.Altitude, 38000);
            /*Assert.Equals(message.TimeFlag, "1111");
            Assert.Equals(message.CprLatitude, "1111");
            Assert.Equals(message.CprLongitude, "1111");
            Assert.Equals(message.CprFormate, "1111");
            Assert.Equals(message.SurveillanceStatus, "1111");*/
        }

        [TestMethod]
        public void PayloadParser_Velocity()
        {
            string adsbSentence = "1379574427.9127481!ADS-B*8D485020994409940838175B284F;";
            IPayloadParser parser = new SimpleVelocityParser();
            IMessageParser payloadParser = new SimpleMessageParser();
            ADSBVelocityMessage message = new ADSBVelocityMessage();
            message.Payload = payloadParser.ParsePayload(adsbSentence).ToBin();

            message = parser.ParseMessage(message) as ADSBVelocityMessage;

           /* Assert.Equals(message.Subtype, "1111");
            Assert.Equals(message.IntentChange, "1111");
            Assert.Equals(message.ReservedA, "1111");
            Assert.Equals(message.NavigationAccuracy, "1111");
            Assert.Equals(message.Speed, "1111");
            Assert.Equals(message.Heading, "1111");
            Assert.Equals(message.VerticalRateSource, "1111");
            Assert.Equals(message.VerticalSpeed, "1111");*/
        }


    }
}
