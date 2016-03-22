using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    class ADSBIdentificationMessage : ADSBMessageBase
    {
        public int EmitterCategory { get; private set; }
        public string AircraftID { get; private set; }

        public ADSBIdentificationMessage(int EmitterCategory, string AircraftID) : base("", "", 0, 0, 0, 0)
        {
            this.EmitterCategory = EmitterCategory;
            this.AircraftID = AircraftID;
        }
    }
}
