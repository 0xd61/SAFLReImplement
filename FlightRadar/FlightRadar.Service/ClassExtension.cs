using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Service
{
    public static class ClassExtension
    {
        /// <summary>
        /// Konvertiert einen string to bin.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToBin(this string value)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in value)
                sb.Append(Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')); //TODO: Besser  machen!

            return sb.ToString();
        }
    }
}
