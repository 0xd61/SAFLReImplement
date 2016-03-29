using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public class ADSBIdentificationMessage : ADSBMessageBase
    {
        public int EmitterCategory { get; set; }
        public string AircraftID { get;  set; }

        public ADSBIdentificationMessage()
        {
        }
    }
}
