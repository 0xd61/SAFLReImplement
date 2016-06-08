using System;
using System.Net;
using System.IO;

namespace FlightRadar.DataAccess
{
    /// <summary>
    /// tries to connect to the server and tries to read the datastream
    /// </summary>
    public class WebMessageRepository : IMessageRepository
    {
        public string ServerURL { get; private set; } = string.Empty;
        private WebRequest request = null;
        private WebResponse response = null;
        private Stream dataStream = null;
        private StreamReader reader = null;

        public WebMessageRepository(string url)
        {
            ServerURL = url;
        }

        public override void StartMessageLoop()
        {
            try
            {
                Connect();
                Connected = true;

                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                Console.WriteLine("Verbunden...");

                MessageLoop();

            }
            catch (Exception)
            {
                Console.WriteLine("Es konnte keine Verbindung hergestellt werden...");
                return;
            }
            finally
            {
                CloseConnection();

            }
        }

        /// <summary>
        /// connects to the server
        /// </summary>
        private void Connect()
        {
            request = WebRequest.Create(ServerURL);

            ((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            response = request.GetResponse();
        }

        /// <summary>
        /// closes the connection
        /// </summary>
        private void CloseConnection()
        {
            reader?.Close();
            response.Close();

            Console.WriteLine("Verbindung geschlossen...");
        }

        /// <summary>
        /// reads messages from messagestream and invokes an event 
        /// </summary>
        private void MessageLoop()
        {
            char[] buffer = new char[100];

            while (StopMessageloop == false)
            {
                string message = ReadMessageStream(buffer);

                RaiseEventMessageReceived(message);
            }
        }

        /// <summary>
        /// reads string from buffer
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private string ReadMessageStream(char[] buffer)
        {
            reader.Read(buffer, 0, 100);
            return new string(buffer);
        }

        /// <summary>
        /// validates message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool IsValidMessage(string message)
        {
            return message.Contains("ADS-B") && message != string.Empty;
        }


        /// <summary>
        /// notifies eventlistener when messageevent is created
        /// </summary>
        /// <param name="message"></param>
        private void RaiseEventMessageReceived(string message)
        {
            if (IsValidMessage(message))
                base.NotifyListener(message);
        }

    }
}
