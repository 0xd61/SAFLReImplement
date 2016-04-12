using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRadar.Model;


namespace FlightRadar.Service
{
    /// <summary>
    /// CPR Berechnung aus 2 Messages 
    /// </summary>
    class CPRCoder
    {
        public const int NZ = 15; //Latitude Zones
        public const double Dlat0 = 360 / 60.0; //EVEN Message (Latitude zone size NORTH/SOUTH)
        public const double Dlat1 = 360 / 59.0; //ODD Message (Latitude zone size NORTH/SOUTH)
        public const double Nb17 = 131072.0; //Number of bits for Encoding

        public static PlanePosition DecodeADSBToPosition(PlanePosition ReferencePosition, ADSBPositionMessage NewMessage)
        {
            double LatitudeReference = ReferencePosition.Latitude;
            double LongitudeReference = ReferencePosition.Longitude;
            int LatitudeCpr = NewMessage.CprLatitude;
            int LongitudeCpr = NewMessage.CprLongitude;
            int Cpr = NewMessage.CprFormate;

            double DLat = (Cpr == 0) ? Dlat0 : Dlat1;
            double j = Math.Floor(LatitudeReference / DLat) + Math.Floor(0.5 + mod(LatitudeReference, DLat) / DLat - LatitudeCpr / Nb17);
            double RLat = (double)DLat * (j + LatitudeCpr / Nb17);

            double Dlon = 360.0 / (NumberOfLongitudeZones.lookup(RLat) - Cpr);
            double m = Math.Floor(LongitudeReference / Dlon) + Math.Floor(0.5 + mod(LongitudeReference, Dlon) / Dlon - LongitudeCpr / Nb17);
            double RLon = (double)Dlon * (m + LongitudeCpr / Nb17);

            return new PlanePosition(NewMessage.Timestamp, (double)RLat, (double)RLon, (double)NewMessage.Altitude);
        }

        public static PlanePosition DecodeGlobalADSB(ADSBPositionMessage OldADSBMessage, ADSBPositionMessage NewADSBMessage)
        {
            if (!OldADSBMessage.ICAO.Equals(NewADSBMessage.ICAO))
            {
                throw new ArgumentException("Not the same ICAO! -> CPRCoder static PlanePosition");
            }

            if (OldADSBMessage.CprFormate == NewADSBMessage.CprFormate)
            {
                throw new ArgumentException("Same CPR - Error!");
            }

            TimeSpan OldMesTimespan = TimeSpan.FromTicks(OldADSBMessage.Timestamp.Ticks);
            TimeSpan NewMesTimespan = TimeSpan.FromTicks(NewADSBMessage.Timestamp.Ticks);

            if ((NewMesTimespan.TotalSeconds - OldMesTimespan.TotalSeconds) > 10)
            {
                throw new ArgumentException("Timestamp too old!");
            }

            int i = NewADSBMessage.CprFormate;

            //Latitude
            double CprLat0 = (i == 0) ? NewADSBMessage.CprLatitude : OldADSBMessage.CprLatitude;
            double CprLat1 = (i == 1) ? NewADSBMessage.CprLatitude : OldADSBMessage.CprLatitude;
            double j = Math.Floor(((59 * CprLat0 - 60 * CprLat1) / Nb17) + 0.5);
            double RLat0 = Dlat0 * (mod(j, 60) + CprLat0 / Nb17);
            double RLat1 = Dlat1 * (mod(j, 59) + CprLat1 / Nb17);

            double latitude = (i == 0) ? RLat0 : RLat1;
            int nl = 0;
            if ((nl = NumberOfLongitudeZones.lookup(RLat0)) != NumberOfLongitudeZones.lookup(RLat1))
            {
                // if the longitude zones are not the same, we cannot calculate the longitude	
                // so we discard this position object
                throw new ArgumentException("CprCoder.decodeGlobalAirborne(): Messages with matching number of longitude zones expected");
            }

            double CprLon0 = (i == 0) ? NewADSBMessage.CprLongitude : OldADSBMessage.CprLongitude;
            double CprLon1 = (i == 1) ? NewADSBMessage.CprLongitude : OldADSBMessage.CprLongitude;
            double m = Math.Floor((CprLon0 * (nl - 1) - CprLon1 * nl) / Nb17 + 0.5);
            double lon = (i == 0) ? CprLon0 : CprLon1;
            double dlon = 360.0 / Math.Max(nl - i, 1);
            double longitude = dlon * (mod(m, nl - i) + lon / Nb17);
            //System.err.println ( String.format("%-8s LAT LON %-3.8f %-3.8f global pos", newMsg.getIcao(), latitude, longitude) );
            return new PlanePosition(NewADSBMessage.Timestamp, (double)latitude, (double)longitude, (double)NewADSBMessage.Altitude);


        }




        private static double mod(double x, double y)
        {
            return (double)x - y * Math.Floor(x / y);
        }
    }
    class NumberOfLongitudeZones
    {
        public static int lookup(double cprLatitude)
        {
            // this lookup procedure associates a number of 
            // longitude (NL) zones with a given latitude
            // taken from 1090-WP-9-14: "Transition Table for NL(lat) Function"
            // this implementation does not work close to the poles (some entries missing).
            double lat = Math.Abs(cprLatitude);
            if (lat < 10.47047130)
                return 59;
            else if (lat < 14.82817437)
                return 58;
            else if (lat < 18.18626357)
                return 57;
            else if (lat < 21.02939493)
                return 56;
            else if (lat < 23.54504487)
                return 55;
            else if (lat < 25.82924707)
                return 54;
            else if (lat < 27.93898710)
                return 53;
            else if (lat < 29.911356862)
                return 52;
            else if (lat < 31.77209708)
                return 51;
            else if (lat < 33.53993436)
                return 50;
            else if (lat < 35.22899598)
                return 49;
            else if (lat < 36.85025108)
                return 48;
            else if (lat < 38.41241892)
                return 47;
            else if (lat < 39.92256684)
                return 46;
            else if (lat < 41.38651832)
                return 45;
            else if (lat < 42.80914012)
                return 44;
            else if (lat < 44.19454951)
                return 43;
            else if (lat < 45.54626723)
                return 42;
            else if (lat < 46.86733252)
                return 41;
            else if (lat < 48.16039128)
                return 40;
            else if (lat < 49.42776439)
                return 39;
            else if (lat < 50.67150166)
                return 38;
            else if (lat < 51.89342469)
                return 37;
            else if (lat < 53.09516153)
                return 36;
            else if (lat < 54.27817472)
                return 35;
            else if (lat < 55.44378444)
                return 34;
            else if (lat < 56.59318756)
                return 33;
            else if (lat < 57.72747354)
                return 32;
            else if (lat < 58.84763776)
                return 31;
            else if (lat < 59.95459277)
                return 30;
            else if (lat < 61.04917774)
                return 29;
            else if (lat < 62.13216659)
                return 28;
            else if (lat < 63.20427479)
                return 27;
            else if (lat < 64.26616523)
                return 26;
            else if (lat < 65.31845310)
                return 25;
            else if (lat < 66.36171008)
                return 24;
            else if (lat < 67.39646774)
                return 23;
            else if (lat < 68.42322022)
                return 22;
            else if (lat < 69.44242631)
                return 21;
            else if (lat < 70.45451075)
                return 20;
            else if (lat < 71.45986473)
                return 19;
            else if (lat < 72.45884545)
                return 18;
            else if (lat < 73.45177442)
                return 17;
            else if (lat < 74.43893416)
                return 16;
            else if (lat < 75.42056257)
                return 15;
            else if (lat < 76.39684391)
                return 14;
            else if (lat < 77.36789461)
                return 13;
            else if (lat < 78.33374083)
                return 12;
            else if (lat < 79.29428225)
                return 11;
            else
                // north/south of 80.24923213: we don't care about the poles
                return 10;
        }

    }
}
