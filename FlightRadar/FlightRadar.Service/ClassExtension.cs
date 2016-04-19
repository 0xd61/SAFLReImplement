using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightRadar.Service
{
    /// <summary>
    /// class used for all class extensions
    /// </summary>
    public static class ClassExtension
    {
        /// <summary>
        /// convert hex string to bin.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToBin(this string value)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in value)
            {
                sb.Append(IntToBinString(CharToHex(c)));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts a char to a integer with a base of 16.
        /// </summary>
        /// <param name="charToConvert">The char which should be converted.</param>
        /// <returns><see cref="int"/></returns>
        private static int CharToHex(char charToConvert)
        {
            return Convert.ToInt32(charToConvert.ToString(), 16);
        }

        /// <summary>
        /// Converts a hex value to a binary string.
        /// </summary>
        /// <param name="charInHex">The integer in hexadecimal.</param>
        /// <returns><see cref="string"/></returns>
        private static string IntToBinString(int hexValue)
        {
            return Convert.ToString(hexValue, 2).PadLeft(4, '0');
        }
    }
}
