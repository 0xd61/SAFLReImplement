using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FlightRadar.Model;

namespace FlightRadar.Service.MessageParser
{
    class SimpleMessageParser : IMessageParser
    {
        /// <summary>
        /// parse String from JSON ADSB Sentence
        /// </summary>
        /// <param name="adsbSentence"></param>
        /// <returns></returns>
        public ADSBRawMessage Parse(string adsbSentence)
        {
            Regex pattern = new Regex(@"\d+\.\d+!ADS-B\*[0-9A-Z]{28}");
            string myString = pattern.Match(adsbSentence).ToString();




            return myString;
        }

        private string ParseTimestamp(string sentence)
        {
            return sentence.Substring(0, 18);
        }

        private string ParseDfca(string sentence)
        {
            return sentence.Substring(25, 2);
        }

        private string ParseIcao(string sentence)
        {
            return sentence.Substring(27, 6);
        }

        private string ParsePayload(string sentence)
        {
            return sentence.Substring(33, 14);
        }

        private string ParsePartiy(string sentence)
        {
            return sentence.Substring(47);
        }

        public string ParseMessagetype(string message)
        {
            string type
            return type;
        }

        public string 
    }
}
