using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public class Plane
    {
        public string ICAO { get; } = string.Empty;
        private ADSBMessageContainer Messages = new ADSBMessageContainer();
        public DateTime Time{ get; private set; }

        public Plane(string ICAO)
        {
            this.ICAO = ICAO;
        }

        public void addMessageToPlane(ADSBMessageBase Message)
        {
            this.Messages.Add(Message);
            this.Time = DateTime.Now;
        }

        public ADSBMessageContainer getADSBMessageContainer()
        {
            return this.Messages;
        }
    }
}
