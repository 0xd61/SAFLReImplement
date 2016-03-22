using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Model
{
    public class ADSBRawMessage : ADSBMessageBase
    {
        public ADSBRawMessage() : base("","",0,0,0,0)
        {

        }
        private ADSBMessagetype type;
    }
}
