using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRadar.Model;
using System.IO;

namespace FlightRadar.DataAccess
{
    public class LocalMessageRepository : IMessageRepository
    {
        public ADSBMessageBase GetMessage()
        {
            StreamReader reader = new StreamReader("D:/testFile.txt");

            ADSBMessageBase result = new ADSBMessageBase { Name = reader.ReadLine() };

            return result;
        }
    }
}
