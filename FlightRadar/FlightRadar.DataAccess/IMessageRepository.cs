namespace FlightRadar.DataAccess
{
    public abstract class IMessageRepository
    {
        public delegate void GetMessageHandler(string message);
        public event GetMessageHandler OnGetMessage;

        public bool StopMessageloop { get; set; } = false;
        public bool Connected { get; set; } = false;

        protected void NotifyListener(string message)
        {
            OnGetMessage?.Invoke(message);
        }

        public abstract void StartMessageLoop();
    }
}
