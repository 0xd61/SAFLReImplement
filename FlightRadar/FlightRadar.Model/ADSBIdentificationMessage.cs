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
