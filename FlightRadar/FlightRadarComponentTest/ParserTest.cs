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

            /*Console.WriteLine(message.AircraftID+"#"+message.EmitterCategory);*/
            Assert.AreEqual(message.AircraftID.ToString(), "KLM1023 ");
            Assert.AreEqual(message.EmitterCategory, 0);
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

            /*Console.Write(  message.NicSupplement + "\n" +
                            message.Altitude + "\n" +
                            message.TimeFlag + "\n" +
                            message.CprLatitude + "\n" +
                            message.CprLongitude + "\n" +
                            message.CprFormate + "\n" +
                            message.SurveillanceStatus);*/

            Assert.AreEqual(message.NicSupplement.ToString(), "0");
            Assert.AreEqual(message.Altitude.ToString(), "3128");
            Assert.AreEqual(message.TimeFlag.ToString(), "0");
            Assert.AreEqual(message.CprLatitude.ToString(), "51372");
            Assert.AreEqual(message.CprLongitude.ToString(), "93000");
            Assert.AreEqual(message.CprFormate.ToString(), "0");
            Assert.AreEqual(message.SurveillanceStatus.ToString(), "0");
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

            /*Console.Write(  message.Subtype +"\n"+
                            message.IntentChange + "\n" +
                            message.ReservedA + "\n" +
                            message.NavigationAccuracy + "\n" +
                            message.Speed + "\n" +
                            message.Heading + "\n" +
                            message.VerticalRateSource + "\n" +
                            message.VerticalSpeed);*/

            Assert.AreEqual(message.Subtype.ToString(), "1");
            Assert.AreEqual(message.IntentChange.ToString(), "0");
            Assert.AreEqual(message.ReservedA.ToString(), "1");
            Assert.AreEqual(message.NavigationAccuracy.ToString(), "0");
            Assert.AreEqual(message.Speed.ToString(), "160");
            Assert.AreEqual(message.Heading.ToString(), "184");
            Assert.AreEqual(message.VerticalRateSource.ToString(), "0");
            Assert.AreEqual(message.VerticalSpeed.ToString(), "-832");
        }


    }
}
